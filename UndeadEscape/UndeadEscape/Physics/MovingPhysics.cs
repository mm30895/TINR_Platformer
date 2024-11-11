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
            movable.Position += movable.Velocity ;
        }

    }

    public static void SimulateMovement(IMovable item, TimeSpan elapsed)
    {
        item.Position += item.Velocity;
    }
}
