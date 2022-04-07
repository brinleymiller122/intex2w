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

        public float city_OUTSIDE_CITY_LIMITS { get; set; }

        public float milepoint_01 {get; set;}

        

        public float county_name_SALT_LAKE { get; set; } 
        
        public float county_name_UTAH { get; set; }

        

        public Tensor<float> AsTensor()
        {
            float[] data = new float[]
            {
            intersection_related,older_driver_involved, night_dark_condition, single_vehicle, roadway_departure, route_15, milepoint_01, city_OUTSIDE_CITY_LIMITS, county_name_SALT_LAKE, county_name_UTAH
            };
            int[] dimensions = new int[] { 1, 11 };
            return new DenseTensor<float>(data, dimensions);
        }
    }

}
