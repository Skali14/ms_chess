using UnityEngine;

public class TurnIndicator : MonoBehaviour
{
    private Game game;
    public Sprite white_sprite;
    public Sprite black_sprite;
    public SpriteRenderer spriteRenderer;
    public RectTransform rectTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        game = Game.instance;
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.sprite = game.IsWhiteTurn ? white_sprite : black_sprite;
        rectTransform.rotation = game.IsWhiteTurn ? new Quaternion(0, 0, 180, 0) : new Quaternion(0, 0, 0, 0);
    }
}
