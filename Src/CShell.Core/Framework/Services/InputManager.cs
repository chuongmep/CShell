using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;
using Caliburn.Micro;
using Microsoft.Xaml.Behaviors;

namespace CShell.Framework.Services
{
	[Export(typeof(IInputManager))]
	public class InputManager : IInputManager
	{
		public void SetShortcut(DependencyObject view, InputGesture gesture, object handler)
		{
			var inputBindingTrigger = new InputBindingTrigger();
			inputBindingTrigger.InputBinding = new InputBinding(new RoutedCommand(), gesture);

			//var target = ViewLocator.LocateForModel(handler, null, null);
			Interaction.GetTriggers(view).Add(inputBindingTrigger);

			inputBindingTrigger.Actions.Add(new TestTriggerAction(handler));
		}

		public void SetShortcut(InputGesture gesture, object handler)
		{
			SetShortcut(Application.Current.MainWindow, gesture, handler);
		}

		private class TestTriggerAction : TriggerAction<FrameworkElement>
		{
			private readonly object _handler;

			public TestTriggerAction(object handler)
			{
				_handler = handler;
			}

			protected override void Invoke(object parameter)
			{
				Action.Invoke(_handler, "Execute");
			}
		}
	}
}