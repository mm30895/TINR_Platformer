using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndeadEscape.Physics;
public static class MovingPhysics
{
    public static void SimulateMovement(object item, TimeSpan elapsed)
    {
        if (item is IMovable movable)
        {
            movable.Position += movable.Velocity * (float)elapsed.TotalSeconds;
        }

    }

    public static void SimulateMovement(IMovable item, TimeSpan elapsed)
    {
        item.Position += item.Velocity * (float)elapsed.TotalSeconds;
    }
    public static void SimulateGravity(IMovable item, TimeSpan elapsed) {
        item.Position += item.Gravity * (float)elapsed.TotalSeconds;
    }
    public static void SimulateGravity(object item, TimeSpan elapsed)
    {
        if (item is IMovable movable)
        {
            movable.Position += movable.Gravity * (float)elapsed.TotalSeconds;
        }

    }
}
