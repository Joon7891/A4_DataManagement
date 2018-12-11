// Author: Joon Song
// File Name: SimulationStatus.cs
// Project Name: A4_DataManagement
// Creation Date: 12/07/2018
// Modified Date: 12/07/2018
// Description: Class to hold SimulationStatus enum

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A4_DataManagement
{
    /// <summary>
    /// Enum containing various simulation status types
    /// </summary>
    public enum SimulationStatus : byte
    {
        NotStarted,
        Playing,
        Paused
    }
}
