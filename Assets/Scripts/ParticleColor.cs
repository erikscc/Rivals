using UnityEngine;

namespace ESCape
{
    public class ParticleColor : MonoBehaviour
    {
        /// <summary>
        /// Array for particle systems that should be colored.
        /// </summary>
        public ParticleSystem[] particles;

        /// <summary>
        /// Iterates over all particles and assigns the color passed in,
        /// but ignoring the alpha value of the new color.
        /// </summary>
        public void SetColor(Color color)
        {
            for(int i = 0; i < particles.Length; i++)
            {
                var main = particles[i].main;
                color.a = main.startColor.color.a;
                main.startColor = color;
            }
        }
    }
}