using System;
using System.Collections;
using Metozis.System.Management;
using UnityEngine;

namespace Metozis.System.Reactive
{
    public static class Invocation
    {
        public static void Delay(float delayTime, Action context)
        {
            MonoBridge.Instance.StartCoroutine(DelayRoutine(delayTime, context));
        }

        private static IEnumerator DelayRoutine(float delay, Action ctx)
        {
            yield return new WaitForSeconds(delay);
            ctx.Invoke();
        }

        public static void Interval(float interval, Action ctx, Func<bool> stop)
        {
            MonoBridge.Instance.StartCoroutine(IntervalRoutine(interval, ctx, stop));
        }
        
        private static IEnumerator IntervalRoutine(float interval, Action ctx, Func<bool> stop)
        {
            while (!stop())
            {
                ctx();
                yield return new WaitForSeconds(interval);
            }
        }
    }
}