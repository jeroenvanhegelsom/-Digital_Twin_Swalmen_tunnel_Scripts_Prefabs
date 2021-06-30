using UnityEngine;
using UnityEngine.UI;

public class GUI_PumpingCellars : MonoBehaviour
{
    [Header("Settings")]

        [SerializeField, Tooltip("Clean water script")]
        private GameObject CleanWater;

        [SerializeField, Tooltip("Full level height of the clean water")]
        private float CleanWaterFull;

        [SerializeField, Tooltip("Dirty water script")]
        private GameObject DirtyWater;

        [SerializeField, Tooltip("Full level height of the dirty water")]
        private float DirtyWaterFull;

        [SerializeField, Tooltip("Text field in GUI where the clean water filling level value is displayed")]
        private Text GUIValueTextClean;

        [SerializeField, Tooltip("Text field in GUI where the dirty water filling level value is displayed")]
        private Text GUIValueTextDirty;

        [SerializeField, Tooltip("Maximum filling velocity")]
        private float v_max;

    private int setting_clean;
    private int setting_dirty;
    private float v_clean;
    private float v_dirty;
    private GameObject cleanWater_filling;
    private GameObject dirtyWater_filling;

    private void Start()
    {
        cleanWater_filling = CleanWater.transform.Find("WaterFilling").gameObject;
        dirtyWater_filling = DirtyWater.transform.Find("WaterFilling").gameObject;
    }

    void FixedUpdate()
    {
        if (CleanWater.gameObject.transform.localPosition.y < CleanWaterFull)
        {
            cleanWater_filling.SetActive(setting_clean > 0);
            CleanWater.gameObject.transform.position += v_clean * Vector3.up * Time.deltaTime;
        }
        
        if (DirtyWater.gameObject.transform.localPosition.y < CleanWaterFull)
        {
            dirtyWater_filling.SetActive(setting_dirty > 0);
            DirtyWater.gameObject.transform.position += v_dirty * Vector3.up * Time.deltaTime;
        }
    }

    public void ButtonPlusClean()
    {
        if (setting_clean < 4)
        {
            setting_clean += 1;
            v_clean = SetVelocity(setting_clean);
            GUIValueTextClean.text = setting_clean.ToString();
        }
    }

    public void ButtonMinusClean()
    {
        if (setting_clean > 0)
        {
            setting_clean -= 1;
            v_clean = SetVelocity(setting_clean);
            GUIValueTextClean.text = setting_clean.ToString();
        }
    }

    public void ButtonPlusDirty()
    {
        if (setting_dirty < 4)
        {
            setting_dirty += 1;
            v_dirty = SetVelocity(setting_dirty);
            GUIValueTextDirty.text = setting_dirty.ToString();
        }
    }

    public void ButtonMinusDirty()
    {
        if (setting_dirty > 0)
        {
            setting_dirty -= 1;
            v_dirty = SetVelocity(setting_dirty);
            GUIValueTextDirty.text = setting_dirty.ToString();
        }
    }

    private float SetVelocity(int setting)
    {
        return setting / 4f * v_max;
    }
}
