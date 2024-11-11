using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UndeadEscape.Physics;
public interface IVelocity
{
    ref Vector2 Velocity { get; }
}
