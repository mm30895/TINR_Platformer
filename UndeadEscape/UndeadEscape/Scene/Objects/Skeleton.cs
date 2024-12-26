using Microsoft.Xna.Framework;
using System;
using UndeadEscape.Physics;
using UndeadEscape.Protocols;

namespace UndeadEscape.Scene.Objects;

public class Skeleton : Entity
{

    private bool isActive;

    public ref bool IsActive => ref isActive;
}