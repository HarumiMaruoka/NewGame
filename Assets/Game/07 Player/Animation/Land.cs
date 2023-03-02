using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Land : StateMachineBehaviour
    {
        private PlayerController _playerController = null;

        // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            if (_playerController == null)
            {
                _playerController = animator.GetComponent<PlayerController>();
            }
            _playerController.PlayerMove.CanMove = false;
            _playerController.PlayerMove.CanJump = false;
        }
        // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
        override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            _playerController.PlayerMove.CanMove = true;
            _playerController.PlayerMove.CanJump = true;
        }
    }
}
