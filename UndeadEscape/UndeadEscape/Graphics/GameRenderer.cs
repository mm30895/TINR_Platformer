using UndeadEscape.Scene;
using UndeadEscape.Scene.Objects;
using UndeadEscape.Protocols;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace UndeadEscape.Graphics;

public class GameRenderer : DrawableGameComponent {
    private SpriteBatch _spriteBatch;
    private AnimatedSprite _playerCharacter_idle;
    private AnimatedSprite _skeleton_idle;
    private AnimatedSprite _playerCharacter_running;
    private Sprite _playerSprite;
    private Sprite _tileSprite;
    private Sprite _skeletonSprite;
    private Sprite _zombieSprite;
    private Sprite _lichSprite;
    private Sprite _backgroundTileSprite;
    private Level _level;

    private int framesInTotal;
    private int frameRunnig = 0;
    private float timeSinceLastFrame = 0;
    


    public GameRenderer(Game game, Level level) : base(game) {
        _level = level;
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        var textureAtlas = Game.Content.Load<Texture2D>("atlas");

        _playerCharacter_idle = new AnimatedSprite
        {
            Texture = textureAtlas,
            WidthPerFrame = 32,
            NumOfFrames = 2,
            FrameCount = 0,
            MilisecondsPerFrame = 600,
            TimeSinceLastFrame = 0,
            SourceRectangle = new Rectangle(0, 0, 32, 32),
            Origin = new Vector2(16, 16)
        };
        _playerCharacter_running = new AnimatedSprite
        {
            Texture = textureAtlas,
            WidthPerFrame = 32,
            NumOfFrames = 6,
            FrameCount = 0,
            MilisecondsPerFrame = 100,
            TimeSinceLastFrame = 0,
            SourceRectangle = new Rectangle(0, 32, 32, 32),
            Origin = new Vector2(16, 16)
        };
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
        _skeleton_idle = new AnimatedSprite
        {
            Texture = textureAtlas,
            WidthPerFrame = 32,
            NumOfFrames = 2,
            FrameCount = 0,
            MilisecondsPerFrame = 600,
            TimeSinceLastFrame = 0,
            SourceRectangle = new Rectangle(0, 160, 32, 32),
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
            var itemWithAnimation = item as IAnimation;
            Sprite sprite = null;
            AnimatedSprite animatedSprite = null;
            if (item is BackgroundTile) 
            {
                sprite = _backgroundTileSprite;
            }
            else if (item is PlayerCharacter)
            {
                if (itemWithAnimation.Animation == 0)
                {
                    animatedSprite = _playerCharacter_idle;
                }
                else if (itemWithAnimation.Animation == 1) {
                    animatedSprite = _playerCharacter_running;
                }
                sprite = _playerSprite;
                
            }
            else if (item is Tile) { 
                sprite = _tileSprite;
            }
            else if (item is Skeleton)
            {
                sprite = _skeletonSprite;
                animatedSprite = _skeleton_idle;
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
                if (item is not IAnimation || animatedSprite is null)
                {
                    _spriteBatch.Draw(sprite.Texture, itemWithPosition.Position, sprite.SourceRectangle, Color.White, 0, sprite.Origin, 2, SpriteEffects.None, 0);
                    
                }
                else
                {
                    
                    
                    if (animatedSprite.FrameCount < animatedSprite.NumOfFrames)
                    {
                        _spriteBatch.Draw(animatedSprite.Texture, itemWithPosition.Position, new Rectangle(animatedSprite.SourceRectangle.X + (32 * animatedSprite.FrameCount), animatedSprite.SourceRectangle.Y, animatedSprite.SourceRectangle.Width, animatedSprite.SourceRectangle.Height), Color.White, 0f, animatedSprite.Origin, 2, SpriteEffects.None, 0);
                        animatedSprite.TimeSinceLastFrame += (float)gameTime.ElapsedGameTime.TotalMilliseconds;


                        if (animatedSprite.TimeSinceLastFrame > animatedSprite.MilisecondsPerFrame)
                        {
                            animatedSprite.TimeSinceLastFrame -= animatedSprite.MilisecondsPerFrame;
                            //frameRunnig++;
                            animatedSprite.FrameCount++;
                            if (animatedSprite.FrameCount == animatedSprite.NumOfFrames)
                            {
                                animatedSprite.FrameCount = 0;
                            }
                        }
                    }
                }
            }
        }
        _spriteBatch.End();
    }
}