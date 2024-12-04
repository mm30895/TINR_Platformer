using UndeadEscape.Scene;
using UndeadEscape.Scene.Objects;
using UndeadEscape.Protocols;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using UndeadEscape.Physics;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;

namespace UndeadEscape.Graphics;

public class GameRenderer : DrawableGameComponent {
    private SpriteBatch _spriteBatch;
    private AnimatedSprite _playerCharacter_idle;
    private AnimatedSprite _skeleton_idle;
    private AnimatedSprite _playerCharacter_running;
    private AnimatedSprite _playerCharacter_attack;
    private AnimatedSprite _playerCharacter_falling;
    private AnimatedSprite _playerCharacter_jumping;
    private Sprite _playerSprite;
    private Sprite _tileSprite;
    private Sprite _skeletonSprite;
    private Sprite _zombieSprite;
    private Sprite _lichSprite;
    private Sprite _backgroundTileSprite;
    private Level _level;

    private Texture2D textureAtlas;
    


    public GameRenderer(Game game, Level level) : base(game) {
        _level = level;
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        textureAtlas = Game.Content.Load<Texture2D>("atlas2");

        _playerCharacter_idle = new AnimatedSprite
        {
            Texture = textureAtlas,
            WidthPerFrame = 32*4,
            NumOfFrames = 2,
            FrameCount = 0,
            MilisecondsPerFrame = 600,
            TimeSinceLastFrame = 0,
            SourceRectangle = new Rectangle(4, 0, 32*4, 32 * 4),
            Origin = new Vector2(16 * 4, 16 * 4)
        };
        _playerCharacter_running = new AnimatedSprite
        {
            Texture = textureAtlas,
            WidthPerFrame = 32 * 4,
            NumOfFrames = 6,
            FrameCount = 0,
            MilisecondsPerFrame = 100,
            TimeSinceLastFrame = 0,
            SourceRectangle = new Rectangle(0, 33 * 4, 32 * 4, 32 * 4),
            Origin = new Vector2(16 * 4, 16 * 4)
        };
        _playerCharacter_attack = new AnimatedSprite
        {
            Texture = textureAtlas,
            WidthPerFrame = 32 * 4,
            NumOfFrames = 4,
            FrameCount = 0,
            MilisecondsPerFrame = 100,
            TimeSinceLastFrame = 0,
            SourceRectangle = new Rectangle(0, 65 * 4, 32 * 4, 32 * 4),
            Origin = new Vector2(16 * 4, 16 * 4)
        };
        _playerCharacter_falling = new AnimatedSprite
        {
            Texture = textureAtlas,
            WidthPerFrame = 32 * 4,
            NumOfFrames = 1,
            FrameCount = 0,
            MilisecondsPerFrame = 100,
            TimeSinceLastFrame = 0,
            SourceRectangle = new Rectangle(4, 128 * 4, 32 * 4, 32 * 4),
            Origin = new Vector2(16 * 4, 16 * 4)
        };
        _playerCharacter_jumping = new AnimatedSprite
        {
            Texture = textureAtlas,
            WidthPerFrame = 32 * 4,
            NumOfFrames = 1,
            FrameCount = 0,
            MilisecondsPerFrame = 100,
            TimeSinceLastFrame = 0,
            SourceRectangle = new Rectangle(32*4 + 5, 128 * 4 + 5, 32 * 4, 32 * 4),
            Origin = new Vector2(16 * 4, 16 * 4)
        };
        _playerSprite = new Sprite
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(0, 0, 32 * 4, 32 * 4),
            Origin = new Vector2(16 * 4, 16 * 4)
        };
        _tileSprite = new Sprite
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(320 * 4, 64 * 4, 32 * 4, 32 * 4),
            Origin = new Vector2(16 * 4, 16 * 4)
        };
        _skeleton_idle = new AnimatedSprite
        {
            Texture = textureAtlas,
            WidthPerFrame = 32 * 4,
            NumOfFrames = 2,
            FrameCount = 0,
            MilisecondsPerFrame = 600,
            TimeSinceLastFrame = 0,
            SourceRectangle = new Rectangle(0, 160 * 4, 32 * 4, 32 * 4),
            Origin = new Vector2(16 * 4, 16 * 4)
        };
        _skeletonSprite = new Sprite
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(0, 160 * 4, 32 * 4, 32 * 4),
            Origin = new Vector2(16 * 4, 16 * 4)
        };
        _zombieSprite = new Sprite
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(0, 289 * 4, 32 * 4, 32 * 4),
            Origin = new Vector2(16 * 4, 16 * 4)
        };
        _lichSprite = new Sprite
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(0, 321 * 4, 64 * 4, 64 * 4),
            Origin = new Vector2(32 * 4, 32 * 4)
        };
        _backgroundTileSprite = new Sprite 
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(320 * 4, 32 * 4, 32 * 4, 32 * 4),
            Origin = new Vector2(16 * 4, 16 * 4)
        };

    }
    public override void Draw(GameTime gameTime) {

        GraphicsDevice.Clear(Color.Gray);
        //_spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearClamp);
        _spriteBatch.Begin();
        foreach (object item in _level.Scene)
        {
           

            if (item is Map) {
                //its a map
                var maps = item as Map;
                int displayTilesize = 128;
                int numTilesPerRow = 20;
                int pixelTileSize = 128;
                if (maps.Drawable) { 
                foreach (var num in maps.MapCsv) {
                    Rectangle derct = new Rectangle(
                            (int)num.Key.X * displayTilesize,
                            (int)num.Key.Y * displayTilesize,
                            displayTilesize,
                            displayTilesize
                        );
                    int x = num.Value % numTilesPerRow;
                    int y = num.Value / numTilesPerRow;
                    Rectangle source = new Rectangle(
                        x * pixelTileSize,
                        y * pixelTileSize,
                        pixelTileSize,
                        pixelTileSize
                        );
                    _spriteBatch.Draw(textureAtlas, derct, source, Color.White);
                }
                }
            
            }
            var itemWithPosition = item as IPosition;
            var itemWithAnimation = item as IAnimation;
            var itemWithRotateAnimation = item as IRotateAnimation;
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
                else if (itemWithAnimation.Animation == 1)
                {
                    animatedSprite = _playerCharacter_running;
                }
                else if (itemWithAnimation.Animation == 2) {
                    animatedSprite = _playerCharacter_attack;
                }
                else if (itemWithAnimation.Animation == 3)
                {
                    animatedSprite = _playerCharacter_falling;
                }
                else if (itemWithAnimation.Animation == 4)
                {
                    animatedSprite = _playerCharacter_jumping;
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
                    _spriteBatch.Draw(sprite.Texture, itemWithPosition.Position, sprite.SourceRectangle, Color.White, 0, sprite.Origin, 1, SpriteEffects.None, 0);
                    
                }
                else
                {
                    
                    
                    if (animatedSprite.FrameCount < animatedSprite.NumOfFrames)
                    {
                        if (item is IRotateAnimation && itemWithRotateAnimation.RotateAnimation == 1)

                        {
                            _spriteBatch.Draw(animatedSprite.Texture, itemWithPosition.Position, new Rectangle(animatedSprite.SourceRectangle.X + (32 * 4 * animatedSprite.FrameCount), animatedSprite.SourceRectangle.Y, animatedSprite.SourceRectangle.Width, animatedSprite.SourceRectangle.Height), Color.White, 0f, animatedSprite.Origin, 1, SpriteEffects.FlipHorizontally, 0);
                        }
                        else{
                            _spriteBatch.Draw(animatedSprite.Texture, itemWithPosition.Position, new Rectangle(animatedSprite.SourceRectangle.X + (32 * 4 * animatedSprite.FrameCount), animatedSprite.SourceRectangle.Y, animatedSprite.SourceRectangle.Width, animatedSprite.SourceRectangle.Height), Color.White, 0f, animatedSprite.Origin, 1, SpriteEffects.None, 0);

                        }
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