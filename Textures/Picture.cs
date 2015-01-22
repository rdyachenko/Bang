using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Reflection;

namespace Textures
{
    static public class Picture
    {
        private static global::System.Resources.ResourceManager resourceMan;
        private static global::System.Globalization.CultureInfo resourceCulture;

        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Textures.Properties.Resources", typeof(Picture).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }

        static public Bitmap CartBitmap(int cartID)
        {
            Bitmap bmp = null;
            try
            {
                if ((cartID >= 0 || cartID < 80) || (cartID >= 100 || cartID < 104))
                {
                    string key = string.Format("_{0}", ++cartID);
                    bmp = (Bitmap)ResourceManager.GetObject(key, resourceCulture);
                }
            } catch(Exception){}
            return bmp;
        }

        static public Bitmap CartBitmap(string key)
        {
            Bitmap bmp = null;
            try
            {
                bmp = (Bitmap)ResourceManager.GetObject(key, resourceCulture);
            } catch (Exception){ }
            return bmp;
        }
    }
}

