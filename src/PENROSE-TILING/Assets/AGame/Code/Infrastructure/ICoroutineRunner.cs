using System.Collections;
using UnityEngine;

namespace AGame.Code.Infrastructure
{
  public interface ICoroutineRunner
  {
    Coroutine StartCoroutine(IEnumerator load);
  }
}