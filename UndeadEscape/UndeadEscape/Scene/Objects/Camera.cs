using Microsoft.Xna.Framework;
using System.Collections.Generic;
using UndeadEscape.Physics;

namespace UndeadEscape.Scene.Objects;

public class Camera
{
    private Vector2 _cameraPos;

    public ref Vector2 CameraPos => ref _cameraPos;

}