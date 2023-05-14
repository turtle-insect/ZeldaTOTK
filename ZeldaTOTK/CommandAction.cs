using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZeldaTOTK
{
	internal class CommandAction : ICommand
	{
		private readonly Action<object?> mAction;
		public event EventHandler? CanExecuteChanged;

		public CommandAction(Action<object?> action) => mAction = action;
		public bool CanExecute(object? parameter) => true;
		public void Execute(object? parameter) => mAction(parameter);
	}
}
