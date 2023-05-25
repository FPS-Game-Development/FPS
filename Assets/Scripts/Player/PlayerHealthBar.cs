using UnityEngine;
using UnityEngine.UI;


public class PlayerHealthBar : MonoBehaviour
{
    PlayerStatus playerStatus;
    public Image HealthBar;     //血条Image
    public Image HealthBar_Damage;      //伤害条Image

    public float FadeTime;      //渐变时间
    private bool startDamage = false;

    [SerializeField]
    private float temp;

    private void Start()
    {
        playerStatus = GetComponent<PlayerStatus>();
        HealthBar.fillAmount = 1;
        HealthBar_Damage.fillAmount = HealthBar.fillAmount;
    }

    private void Update()
    {
        FadeValue(HealthBar.fillAmount, FadeTime);
    }

    //伤害条缓变
    public void FadeValue(float endValue, float duration)
    {
        if (startDamage)
        {
            HealthBar_Damage.fillAmount -= (temp / duration) * Time.deltaTime;    //temp/duration使用固定渐变的时间。
            if (HealthBar_Damage.fillAmount <= endValue)        //到达设定值，关闭渐变。
            {
                startDamage = false;
            }
        }
    }

    //受到伤害，先立刻减实际血条，再缓减伤害条
    public void BarDamage()
    {
        HealthBar.fillAmount = playerStatus.HP / playerStatus.maxHP;        //减少血条
        temp = HealthBar_Damage.fillAmount - HealthBar.fillAmount;
        startDamage = true;       //开启伤害渐变
    }
}

