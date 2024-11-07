using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace UndeadEscape.Graphics;
public class AnimatedSprite
{
    private Texture2D _texture;
    private Rectangle _sourceRectangle;
    private Vector2 _origin;
    private Int32 _widthPerFrame;
    private Int32 _numOfFrames;
    private Int32 _frameCount;
    private float _milisecondsPerFrame;
    private float _timeSinceLastFrame;
    public Texture2D Texture 
    {
        get => _texture;
        set => _texture = value;
    }
    public Int32 WidthPerFrame
    {
        get => _widthPerFrame;
        set => _widthPerFrame = value;
    }
    public Int32 NumOfFrames
    {
        get => _numOfFrames;
        set => _numOfFrames = value;
    }
    public Int32 FrameCount
    { 
        get => _frameCount;
        set => _frameCount = value;
    }
    public float MilisecondsPerFrame { 
        get => _milisecondsPerFrame;
        set => _milisecondsPerFrame = value;
    }
    public float TimeSinceLastFrame {
        get => _timeSinceLastFrame;
        set => _timeSinceLastFrame = value;
    }
    public Rectangle SourceRectangle
    {
        get => _sourceRectangle;
        set => _sourceRectangle = value;
    }

    public Vector2 Origin
    {
        get => _origin;
        set => _origin = value;
    }
}
