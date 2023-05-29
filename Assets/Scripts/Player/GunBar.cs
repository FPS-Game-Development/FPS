using UnityEngine;
using UnityEngine.UI;


public class GunBar : MonoBehaviour
{
    GunController gunController;
    public Image GunBar_l;     //血条Image
    public Image GunBar_Damage;      //伤害条Image

    public float FadeTime;      //渐变时间
    private bool startDamage = false;

    [SerializeField]
    private float temp;

    private void Start()
    {
        gunController = GetComponent<GunController>();
        GunBar_l.fillAmount = 1;
        GunBar_Damage.fillAmount = GunBar_l.fillAmount;
    }

    private void Update()
    {
        FadeValue(GunBar_l.fillAmount, FadeTime);
    }

    //伤害条缓变
    public void FadeValue(float endValue, float duration)
    {
        if (startDamage)
        {
            GunBar_Damage.fillAmount -= (temp / duration) * Time.deltaTime;    //temp/duration使用固定渐变的时间。
            if (GunBar_Damage.fillAmount <= endValue)        //到达设定值，关闭渐变。
            {
                startDamage = false;
            }
        }
    }

    //受到伤害，先立刻减实际血条，再缓减伤害条
    public void BarDamage()
    {
        GunBar_l.fillAmount = gunController.Magazine / gunController.MaxMagazine;        //减少血条
        temp = GunBar_Damage.fillAmount - GunBar_l.fillAmount;
        startDamage = true;       //开启伤害渐变
    }
    public void BarLoad(){
        GunBar_l.fillAmount = gunController.Magazine / gunController.MaxMagazine;        //重制血条
        temp = GunBar_Damage.fillAmount - GunBar_l.fillAmount;
    }
}

