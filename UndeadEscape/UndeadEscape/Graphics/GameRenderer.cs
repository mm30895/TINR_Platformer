using UndeadEscape.Scene;
using UndeadEscape.Scene.Objects;
using UndeadEscape.Protocols;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UndeadEscape.Graphics;

public class GameRenderer : DrawableGameComponent {
    private SpriteBatch _spriteBatch;
    private Sprite _playerSprite;
    private Sprite _tileSprite;
    private Sprite _skeletonSprite;
    private Sprite _zombieSprite;
    private Sprite _lichSprite;
    private Sprite _backgroundTileSprite;
    private Level _level;
    


    public GameRenderer(Game game, Level level) : base(game) {
        _level = level;
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        var textureAtlas = Game.Content.Load<Texture2D>("atlas");

        _playerSprite = new Sprite
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(0, 0, 32, 32),
            Origin = new Vector2(16, 16)
        };
        _tileSprite = new Sprite
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(320, 64, 32, 32),
            Origin = new Vector2(16, 16)
        };
        _skeletonSprite = new Sprite
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(0, 160, 32, 32),
            Origin = new Vector2(16, 16)
        };
        _zombieSprite = new Sprite
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(0, 289, 32, 32),
            Origin = new Vector2(16, 16)
        };
        _lichSprite = new Sprite
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(0, 321, 64, 64),
            Origin = new Vector2(32, 32)
        };
        _backgroundTileSprite = new Sprite 
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(320, 32, 32, 32),
            Origin = new Vector2(16, 16)
        };

    }
    public override void Draw(GameTime gameTime) {

        GraphicsDevice.Clear(Color.White);
        _spriteBatch.Begin();
        foreach (object item in _level.Scene)
        {
            var itemWithPosition = item as IPosition;
            Sprite sprite = null;
            if (item is BackgroundTile) 
            {
                sprite = _backgroundTileSprite;
            }
            else if (item is PlayerCharacter)
            {
                sprite = _playerSprite;
            }
            else if (item is Tile) { 
                sprite = _tileSprite;
            }
            else if (item is Skeleton)
            {
                sprite = _skeletonSprite;
            }
            else if (item is Zombie)
            {
                sprite = _zombieSprite;
            }
            else if (item is Lich)
            {
                sprite = _lichSprite;
            }


            if (item is IPosition && sprite is not null)
            {
                _spriteBatch.Draw(sprite.Texture, itemWithPosition.Position, sprite.SourceRectangle, Color.White, 0, sprite.Origin, 2, SpriteEffects.None, 0);
            }
        }
        _spriteBatch.End();
    }
}