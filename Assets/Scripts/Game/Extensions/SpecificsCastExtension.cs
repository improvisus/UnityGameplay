using System.Collections.Generic;
using UnityEngine;

namespace Game.Extensions
{
    public static class SpecificsCastExtension
    {
        // Тут не совсем конус т.к. на конце конуса полусфера, но это для уменьшения вычислений
        public static RaycastHit[] ConeCastAll(Vector3 origin, Vector3 direction, float length, float angle, LayerMask layerMask, int hitBufferSize)
        {
            var resultHits = new List<RaycastHit>();
            
            var radius = length * Mathf.Tan(angle / 2 * Mathf.Deg2Rad);
            
            var sphereCastHits = new RaycastHit[hitBufferSize + 1];
            var count = Physics.SphereCastNonAlloc(origin - direction * radius, radius, direction, sphereCastHits, length + radius, layerMask);
            
            if (count > 0)
            {
                var halfAngle = angle / 2;
                foreach (var hit in sphereCastHits)
                {
                    if(hit.point == Vector3.zero)
                        continue;
                    
                    var directionToHit = hit.point - origin;
                    var angleToHit = Vector3.Angle(direction, directionToHit);

                    if (angleToHit < halfAngle)
                        resultHits.Add(hit);
                }
            }

            return resultHits.ToArray();
        }
    }
}