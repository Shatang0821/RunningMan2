using System.Collections.Generic;
using FrameWork.Utils;
using UnityEngine;

namespace FrameWork.FSM
{
    public abstract class StateMachine<TStateEnum>
    {
        public IState CurrentState { get; private set; }     //現在状態クラス
        
        private Dictionary<TStateEnum, IState> _stateTable = new();
        
        /// <summary>
        /// 状態の初期化
        /// </summary>
        /// <param name="startState"></param>
        public void Initialize(TStateEnum startState)
        {
            ChangeState(startState);
        }
        public void ChangeState(TStateEnum newState)
        {
            if (_stateTable.TryGetValue(newState, out IState state))
            {
                if (CurrentState != state)
                {
                    CurrentState?.Exit();
                    CurrentState = state;

                    CurrentState.Enter();
                }
            }
            
        }

        public void LogicUpdate()
        {
            CurrentState?.LogicUpdate();
        }

        public void PhysicsUpdate()
        {
            CurrentState?.PhysicsUpdate();
        }

        protected void RegisterState(TStateEnum stateEnum, IState state)
        {
            _stateTable[stateEnum] = state;
        }
    }
}