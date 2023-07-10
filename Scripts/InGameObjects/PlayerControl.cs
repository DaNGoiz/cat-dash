using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Mac
using System.Runtime.InteropServices;

public class PlayerControl : MonoBehaviour
{
    /*TODO
    o 控制鼠标灵敏度
    o 设置屏幕边界的碰撞体
    o 玩家根据鼠标方向旋转
    o 解决碰撞体无效问题 （需要2D碰撞体）
    o 玩家 360 度旋转
    o 玩家碰到目标点后的回弹
    o 计时器系统
    o 生成新障碍物
    o 生成新目标点
    o 流程控制
    o 障碍物按方向倒塌
    o 为障碍物分类（可推动（饼），不可推动<伤害玩家，无伤害障碍>）
    o 鼠标出界问题
    o 开始界面制作
    o 猫猫转向bug
    o 障碍物的具体制作
    o 猫的位移问题
    o 音效
    o 猫猫死亡动画
    */

    // Mac
    [DllImport("/System/Library/Frameworks/ApplicationServices.framework/Versions/A/Frameworks/CoreGraphics.framework/CoreGraphics")]
    private static extern void CGWarpMouseCursorPosition(Vector2 point);

    // Windows
    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    public float speed = 0.05f;
    public float rotationSpeed = 0.4f;

    public Sprite catBall;
    public Sprite catBallFall;
    public Sprite catBallDead;

    Vector3 currentMousePosition;
    Vector3 lastMousePosition;
    Vector3 mouseDirection;

    Vector3 objectDirection;
    Vector3 lastObjectDirection;

    StatusManager statusManager;
    private int resetCursor;
    private void Start()
    {
        lastMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        currentMousePosition = lastMousePosition;
        statusManager = GameObject.FindWithTag(TagName.manager).GetComponent<StatusManager>();
    }

    void Update()
    {
        if(statusManager.gameStatus == StatusManager.GameStatus.Playing)
        {
            if (resetCursor >= 30)
            {
                resetCursor = 0;
                SetMousePosition(Screen.width / 2, Screen.height / 2);
                lastMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            }
            resetCursor++;
            Move();
        }
    }
    void Move()
    {
        currentMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        

        if (currentMousePosition != lastMousePosition)
        {
            mouseDirection = (currentMousePosition - lastMousePosition).normalized;
            
            // Transform
            transform.Translate(mouseDirection * speed, Space.World);

            // Rotate

            GameObject.Find("StatusManager").GetComponent<StatusManager>().playerPosition = transform.position;

            objectDirection = transform.position - lastObjectDirection;
            Quaternion rotation = Quaternion.LookRotation(objectDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, rotation.z * 360f), rotationSpeed);
            GameObject.Find("StatusManager").GetComponent<StatusManager>().playerRotation = rotation;
        }
        /*
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Mac
                //CGWarpMouseCursorPosition(new Vector2(Screen.width / 2, Screen.height / 2));

                // Win
                SetMousePosition(Screen.width / 2, Screen.height / 2);
            }
        }
        */
        lastObjectDirection = objectDirection;
        lastMousePosition = currentMousePosition;
    }

    private void SetMousePosition(int x, int y)
    {
        SetCursorPos(x, y);
    }

    public void FallDown()
    {
        StartCoroutine(ScaleChange());
    }
    
    IEnumerator ScaleChange(){
        float time = 0;

        gameObject.GetComponent<SpriteRenderer>().sprite = catBallFall;

        // == 移动的第一部分 ==
        //获得物体的移动方向
        Vector3 playerDirection = GameObject.Find("StatusManager").GetComponent<StatusManager>().playerPosition;
        //从statutsmanager获取displayrotation
        Quaternion displayRotation = GameObject.Find("StatusManager").GetComponent<StatusManager>().displayDirection;
        
        //将player往displayrotation的反方向移动
        transform.position = playerDirection + displayRotation * Vector3.up * 1.2f;

        while(time <= 0.05){
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(3f, 0.8f, 1f), time);
            yield return null;
        }
        while(time > 0.05 && time <= 0.1){
            time += Time.deltaTime;
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(0.8f, 3f, 1f), time);
            yield return null;
        }
        
        // == 移动的第二部分 ==
        //获取物体的rigidbody2D
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        //给物体一个力，使其向displayrotation的方向倒下
        rigidbody2D.AddForce(displayRotation * Vector3.up * 50f);
        
        //玩家没有碰撞体积 = 无敌帧？

        gameObject.GetComponent<SpriteRenderer>().sprite = catBall;
        transform.localScale = new Vector3(1, 1, 1);
    }

    public void Dead(){
        gameObject.GetComponent<SpriteRenderer>().sprite = catBallDead;
        GetComponent<Rigidbody2D>().gravityScale = 3f;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(1500f, 1500f));
        GetComponent<Collider2D>().enabled = false;
    }
    public void Revive()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = catBall;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }
}
