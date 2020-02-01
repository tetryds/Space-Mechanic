using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Astronaut
{
    class AstronautAnimController
    {
        int fwdKey = Animator.StringToHash("Forward");
        int rightKey = Animator.StringToHash("Right");
        int turnRightKey = Animator.StringToHash("TurnRight");
        int upKey = Animator.StringToHash("Up");

        Animator anim;

        float smoothFactor = 2;

        public AstronautAnimController(Animator anim)
        {
            this.anim = anim;
        }

        public void Forward(float value)
        {
            float current = anim.GetFloat(fwdKey);
            current += (value - current) * smoothFactor * Time.fixedDeltaTime;
            anim.SetFloat(fwdKey, current);
        }

        public void Right(float value)
        {
            float current = anim.GetFloat(rightKey);
            current += (value - current) * smoothFactor * Time.fixedDeltaTime;
            anim.SetFloat(rightKey, current);
        }

        public void TurnRight(float value)
        {
            float current = anim.GetFloat(turnRightKey);
            current += (value - current) * smoothFactor * Time.fixedDeltaTime;
            anim.SetFloat(turnRightKey, current);
        }

        public void Up(float value)
        {
            float current = anim.GetFloat(upKey);
            current += (value - current) * smoothFactor * Time.fixedDeltaTime;
            anim.SetFloat(upKey, current);
        }
    }
}
