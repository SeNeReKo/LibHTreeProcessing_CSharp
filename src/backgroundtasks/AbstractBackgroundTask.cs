using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace LibHTreeProcessing.src.backgroundtasks
{

	public abstract class AbstractBackgroundTask : IBackgroundTask
	{

		public event OnBackgroundTaskDelegate OnBackgroundTaskStarted;
		public event OnBackgroundTaskDelegate OnBackgroundTaskCompleted;

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		private readonly Control parent;
		private volatile EnumBackgroundTaskState state;
		protected volatile bool bTerminate;
		private System.Threading.Thread t;
		private volatile Exception error;

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		public AbstractBackgroundTask(Control parent, string name)
		{
			this.parent = parent;
			this.Name = name;
			this.Output = new StringBuilder();
		}

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		public Exception Error
		{
			get {
				return error;
			}
		}

		public string Name
		{
			get;
			private set;
		}

		public StringBuilder Output
		{
			get;
			private set;
		}

		public EnumBackgroundTaskState State
		{
			get {
				return state;
			}
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		private void __FireOnBackgroundTaskStarted()
		{
			try {
				OnBackgroundTaskStarted(this);
			} catch (Exception ee) {
			}
		}

		private void __FireOnBackgroundTaskCompleted()
		{
			try {
				OnBackgroundTaskCompleted(this);
			} catch (Exception ee) {
			}
		}

		private void __Run()
		{
			if (OnBackgroundTaskStarted != null) {
				parent.Invoke(new System.Threading.ThreadStart(__FireOnBackgroundTaskStarted));
			}

			try {
				Run();

				state = EnumBackgroundTaskState.Completed;
			} catch (Exception ee) {
				state = EnumBackgroundTaskState.Failed;
				error = ee;
			}

			if (OnBackgroundTaskCompleted != null) {
				parent.Invoke(new System.Threading.ThreadStart(__FireOnBackgroundTaskCompleted));
			}
		}

		public abstract void Run();

		public void Terminate()
		{
			bTerminate = true;
		}

		public void Start()
		{
			if (state != EnumBackgroundTaskState.None)
				throw new Exception("Invalid state: " + state);

			state = EnumBackgroundTaskState.Running;

			t = new System.Threading.Thread(new System.Threading.ThreadStart(__Run));

			t.Start();
		}

	}

}
