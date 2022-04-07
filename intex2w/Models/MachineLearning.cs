using Microsoft.ML.OnnxRuntime.Tensors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace intex2w.Models
{
    public class MachineLearning
    {
        public float intersection_related { get; set; }

        public float teenage_driver_involved { get; set; }

        public float older_driver_involved { get; set; }

        public float night_dark_condition { get; set; }

        public float single_vehicle { get; set; }

        public float roadway_departure { get; set; }

        public float route_15 { get; set; }

        public float milepoint_0.1 {get; set;}

        public float city_OUTSIDE\ CITY\ LIMITS { get; set; }

        public float county_name_SALT LAKE { get; set; } 
        
        public float county_name_UTAH { get; set; }

        public float crash_severity_id { get; set; }
    }

    public Tensor<float> AsTensor()
    {
        float[] data = new float[]
        {

        };
        int[] dimensions = new int[] { 1, 8 };
        return new DenseTensor<float>(data, dimensions);
    }
}
