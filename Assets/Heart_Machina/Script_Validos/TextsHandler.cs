using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using InputDevice = UnityEngine.XR.InputDevice;
using UnityEngine.InputSystem;

public class TextsHandler : MonoBehaviour
{
   [SerializeField] private GameObject[] _texts;
   [SerializeField] private InputActionReference _buttonReference;
   public static bool _textsActive = true;

   private void Start()
   {
      _buttonReference.action.started += SetTexts;
   }

   private void OnDestroy()
   {
      _buttonReference.action.started -= SetTexts;
   }

   private void SetTexts(InputAction.CallbackContext ctx)
   {
      _textsActive = !_textsActive;
      for (int i = 0; i < _texts.Length; i++)
      {
         _texts[i].SetActive(_textsActive);
      }
   }


   
}
