using System;
using System.Diagnostics;

namespace HistoryDataSample
{
  class Program
  {
    static void Main(string[] args)
    {
      // Get the bytes from a .hst file on the file-system.
      // Or you could get these from a suitable 'blob' field in a database.
      var bytes = System.IO.File.ReadAllBytes("..\\..\\..\\R210318093.hst");

      // Loads an Adaptive history from the bytes.
      // This method is available thanks to a reference in this project
      // to the file HistoryViewer.exe
      // Because HV.exe is compiled specifically for x86, this project must also be fixed as x86.
      var history = new History.AdaptiveHistory(bytes);

      // The history is a large 'spreadsheet' in memory, with a row for each time data was logged
      // and a column for each property whose value is logged.
      // The number of rows is in history.Count and the number of columns is in history.Properties.Count
      // The overall StartTime, EndTime and Duration values are also readily available.
      // StartTime and EndTime are measured in UTC, so will for example be always 5 hours
      // more than Eastern Standard Time.
      //
      // The set of properties vary quite a lot between different histories, but will be
      // the same for histories run on the same kind of machine with the same software control version.
      // 
      // Some of the properties are always present and are also quite interesting, being
      // a record of the progress of the main batch control.  Look for property starting "Parent."

      // Here is a typical loop over all the rows, reading the step number property.
      // Get the property of interest outside the loop for speed.
      var stepNumberProp = history.Properties["Parent.StepNumber"];
      foreach (TimeSpan t in history)
      {
        // t will be the log-time measured since the start of the history.
        // So it starts at 0 and ends at history.Duration
        var stepNumber = stepNumberProp.GetValue(t);  // the step number at this time
        Debugger.Break();
      }
    }
  }
}
