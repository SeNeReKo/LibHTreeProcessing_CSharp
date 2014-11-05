using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace LibHTreeProcessing.src.backgroundtasks
{

	public partial class BackgroundTaskList : UserControl
	{

		public class TaskList
		{
			private BackgroundTaskList parent;

			internal List<IBackgroundTask> tasks;

			internal TaskList(BackgroundTaskList parent)
			{
				this.parent = parent;

				tasks = new List<IBackgroundTask>();
			}

			public void Add(IBackgroundTask task)
			{
				tasks.Add(task);

				parent.__Refill();
			}

			public void Remove(IBackgroundTask task)
			{
				tasks.Remove(task);

				parent.__Refill();
			}

			public void Clean(bool bRemoveCompleted, bool bRemoveFailed)
			{
				for (int i = tasks.Count - 1; i >= 0; i--) {
					switch (tasks[i].State) {
						case EnumBackgroundTaskState.Completed:
							if (bRemoveCompleted) tasks.RemoveAt(i);
							break;
						case EnumBackgroundTaskState.Failed:
							if (bRemoveFailed) tasks.RemoveAt(i);
							break;
					}
				}

				parent.__Refill();
			}
		}

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public BackgroundTaskList()
		{
			InitializeComponent();

			Tasks = new TaskList(this);
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		[Browsable(false)]
		public TaskList Tasks
		{
			get;
			private set;
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		protected void __Refill()
		{
			Controls.Clear();
			int x = 0;
			foreach (IBackgroundTask task in Tasks.tasks) {
				BackgroundTaskLabel label = new BackgroundTaskLabel(task);
				label.Location = new Point(x, 0);
				x += label.Size.Width;
				Controls.Add(label);
			}
		}

	}

}
