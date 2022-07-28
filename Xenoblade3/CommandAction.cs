using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Xenoblade3
{
	internal class CommandAction : ICommand
	{
		private readonly Action<object?> mAction;
		public event EventHandler? CanExecuteChanged;
		public bool CanExecute(object? parameter) => true;
		public CommandAction(Action<object?> action) => mAction = action;
		public void Execute(object? parameter) => mAction(parameter);
	}
}
