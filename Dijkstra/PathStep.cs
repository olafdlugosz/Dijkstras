using System;
using System.Collections.Generic;
using System.Text;

namespace Dijkstra
{
   public class PathStep
    {
        public PathStep(string location, string previousStep) {
            Location = location;
            PreviousStep = previousStep;
        }

        public string Location { get; set; }
        public string PreviousStep { get; set; }
    }
}
