// ------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation.  
//  All Rights Reserved.
//  Licensed under the MIT License.
//  See License in the project root for license information.
// ------------------------------------------------------------------------------

namespace Microsoft.Groove.Api.Samples.Converters
{
    using System;
    //using System.Drawing;

    public class ImageUrlSizeConverter 
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            string imageUrl = value as string;
            int size = int.Parse((string) parameter);
            if (imageUrl != null)
            {
                return null;// new Bitmap((new Uri($"{imageUrl}&w={size}&h={size}").ToString()));
            }

            return null;
        }

        // No need to implement converting back on a one-way binding
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return null;
        }
    }
}
