using UnityEngine;

[System.Serializable]
public class FarmCropDefinition
{
    private const int LegacyMaxGrowthStage = 4;

    public FarmCropType CropType;
    public ItemData HarvestItem;
    [Min(1)] public int DaysPerGrowthStage = 1;
    public Sprite[] StageSprites;

    [SerializeField, HideInInspector] private Sprite Stage00Sprite;
    [SerializeField, HideInInspector] private Sprite Stage01Sprite;
    [SerializeField, HideInInspector] private Sprite Stage02Sprite;
    [SerializeField, HideInInspector] private Sprite Stage03Sprite;
    [SerializeField, HideInInspector] private Sprite Stage04Sprite;

    public int MaxGrowthStage
    {
        get
        {
            if (StageSprites != null && StageSprites.Length > 0)
            {
                return StageSprites.Length - 1;
            }

            return LegacyMaxGrowthStage;
        }
    }

    public Sprite GetStageSprite(int growthStage)
    {
        if (StageSprites != null && StageSprites.Length > 0)
        {
            int stageIndex = Mathf.Clamp(growthStage, 0, StageSprites.Length - 1);
            return StageSprites[stageIndex];
        }

        return growthStage switch
        {
            0 => Stage00Sprite,
            1 => Stage01Sprite,
            2 => Stage02Sprite,
            3 => Stage03Sprite,
            4 => Stage04Sprite,
            _ => null
        };
    }
}
