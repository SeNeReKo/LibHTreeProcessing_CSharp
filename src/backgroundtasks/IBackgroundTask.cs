﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace LibHTreeProcessing.src.backgroundtasks
{

	public interface IBackgroundTask
	{

		event OnBackgroundTaskDelegate OnBackgroundTaskStarted;
		event OnBackgroundTaskDelegate OnBackgroundTaskCompleted;

		event OnTaskProgressDelegate OnProgress;

		////////////////////////////////////////////////////////////////
		// Constants
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Variables
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Constructors
		////////////////////////////////////////////////////////////////

		////////////////////////////////////////////////////////////////
		// Properties
		////////////////////////////////////////////////////////////////

		ArgumentDescription[] ArgumentDescriptions
		{
			get;
		}

		StringBuilder Output
		{
			get;
		}

		Exception Error
		{
			get;
		}

		string Name
		{
			get;
		}

		EnumBackgroundTaskState State
		{
			get;
		}

		////////////////////////////////////////////////////////////////
		// Methods
		////////////////////////////////////////////////////////////////

		void Terminate();

		void Start(ArgumentList arguments);

	}

}
