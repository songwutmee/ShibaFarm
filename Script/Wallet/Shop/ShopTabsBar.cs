using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteAlways]
public class ShopTabsBar : MonoBehaviour
{
    [Header("Padding (px)")]
    public float paddingLeft = 10f;
    public float paddingRight = 10f;
    public float paddingTop = 8f;
    public float paddingBottom = 8f;

    [Header("Spacing (px)")]
    public float spacingX = 8f;
    public float spacingY = 6f;

    [Header("Size")]
    public bool autoWidthFromText = false;
    public float fixedWidth = 120f;
    public float height = 40f;
    public float extraTextWidth = 24f;

    [Header("Behavior")]
    public bool wrapToNextLine = false;
    public bool alignTop = true;

    RectTransform RT => transform as RectTransform;

    void OnEnable() { LayoutChildren(); }
    void OnTransformChildrenChanged() { LayoutChildren(); }
#if UNITY_EDITOR
    void OnValidate() { LayoutChildren(); }
#endif
    void OnRectTransformDimensionsChange() { LayoutChildren(); }

    public void LayoutChildren()
    {
        if (RT == null) return;

        Vector2 pivot = new Vector2(0f, 1f);
        float contentWidth = Mathf.Max(0f, RT.rect.width - paddingLeft - paddingRight);

        float x = paddingLeft;
        float y = -paddingTop;

        for (int i = 0; i < RT.childCount; i++)
        {
            var child = RT.GetChild(i) as RectTransform;
            if (child == null || !child.gameObject.activeSelf) continue;

            child.anchorMin = new Vector2(0f, 1f);
            child.anchorMax = new Vector2(0f, 1f);
            child.pivot = pivot;
            child.localScale = Vector3.one;


            float w = fixedWidth;
            if (autoWidthFromText)
            {
                float textW = 60f;
                var tmp = child.GetComponentInChildren<TextMeshProUGUI>(true);
                if (tmp != null)
                {
                    #if UNITY_EDITOR

                    tmp.ForceMeshUpdate();

                    #endif
                    textW = tmp.preferredWidth;
                }
                w = Mathf.Ceil(textW + extraTextWidth);
            }

            float h = height;

            if (wrapToNextLine && (x - paddingLeft + w > contentWidth) && (x > paddingLeft))
            {
                x = paddingLeft;
                y -= (h + spacingY);
            }

            child.sizeDelta = new Vector2(w, h);
            child.anchoredPosition = new Vector2(x, y);

            x += w + spacingX;
        }

        //float usedRows = 1f;
        if (wrapToNextLine)
        {
            float totalUsedHeight = paddingTop + (-y) + hLast(RT) + paddingBottom;
            var sd = RT.sizeDelta;
            RT.sizeDelta = new Vector2(sd.x, totalUsedHeight);
        }

        float hLast(RectTransform t)
        {
            return height;
        }
    }
}
