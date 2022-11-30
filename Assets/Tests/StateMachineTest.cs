using System.Collections;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests
{
    public class StateMachineTest
    {
        [Test]
        public void StateMachineTransitionChange()
        {
            //Arrange
            var stateMachine = new StateMachine();
            var from = new MockFromStateObject();
            var to = new MockToStateObject();
            stateMachine.AddTransition(from,to,()=>true);
            stateMachine.SetState(from);
            //Act
            stateMachine.Tick();
            //Assert
            Assert.AreEqual(to.WasEntered, true);
        }
        
        [Test]
        public void StateMachineTransitionNoChange()
        {
            //Arrange
            var stateMachine = new StateMachine();
            var from = new MockFromStateObject();
            var to = new MockToStateObject();
            stateMachine.AddTransition(from,to,()=>true);
            stateMachine.SetState(from);
            //Act
            
            //Assert
            Assert.AreEqual(to.WasEntered, false);
        }
        
    }
    
    
    public class MockFromStateObject: IState
    {
        public void Tick()
        {
            
        }
        public void OnEnter()
        {
            
        }
        public void OnExit()
        {
            
        }
    }
    
    public class MockToStateObject: IState
    {
        public bool WasEntered;
        public void Tick()
        {
            
        }
        public void OnEnter()
        {
            WasEntered = true;
        }
        public void OnExit()
        {
            
        }
    }
}
