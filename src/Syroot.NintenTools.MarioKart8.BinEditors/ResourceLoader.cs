using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace Syroot.NintenTools.MarioKart8.BinEditors
{
    /// <summary>
    /// Represents the resources of an assembly and provides helper methods to read specific data from the embedded
    /// resources.
    /// </summary>
    public class ResourceLoader
    {
        // ---- CONSTRUCTORS -------------------------------------------------------------------------------------------

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceLoader"/> class for the given
        /// <paramref name="assembly"/> and accessing resource for the provided <paramref name="resourceNamespace"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> which resource will be accessible.</param>
        /// <param name="resourceNamespace">The namespace in which resources are searched in.</param>
        public ResourceLoader(Assembly assembly, string resourceNamespace)
        {
            Assembly = assembly;
            ResourceNamespace = resourceNamespace;
            ResourceNames = new ReadOnlyCollection<string>(Assembly.GetManifestResourceNames());
        }

        // ---- PROPERTIES ---------------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the <see cref="Assembly"/> which resource are accessible.
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Gets the root namespace in which embedded resources reside.
        /// </summary>
        public string ResourceNamespace { get; }

        /// <summary>
        /// Gets the list of available resource names.
        /// </summary>
        public ReadOnlyCollection<string> ResourceNames { get; }

        // ---- METHODS (PUBLIC) ---------------------------------------------------------------------------------------

        /// <summary>
        /// Gets the bitmap from the resource with the specified name.
        /// </summary>
        /// <param name="resourceName">The name of the bitmap resource.</param>
        /// <returns>The bitmap resource.</returns>
        public Bitmap GetBitmap(string resourceName)
        {
            using (Stream stream = GetStream(resourceName))
            {
                return (Bitmap)Image.FromStream(stream);
            }
        }

        /// <summary>
        /// Gets the icon from the resource with the specified name.
        /// </summary>
        /// <param name="resourceName">The name of the icon resource.</param>
        /// <returns>The icon resource.</returns>
        public Icon GetIcon(string resourceName)
        {
            using (Stream stream = GetStream(resourceName))
            {
                return new Icon(stream);
            }
        }

        /// <summary>
        /// Gets the string from the resource with the specified name.
        /// </summary>
        /// <param name="resourceName">The name of the text file resource.</param>
        /// <returns>The text file resource contents.</returns>
        public string GetString(string resourceName)
        {
            using (Stream stream = GetStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Gets the stream from the resource with the specified name.
        /// </summary>
        /// <param name="resourceName">The name of the resource.</param>
        /// <returns>The streamed resource contents.</returns>
        public Stream GetStream(string resourceName)
        {
            // Check if the resource is available in the assembly and return a stream if possible.
            resourceName = ResourceNamespace + "." + resourceName;
            if (ResourceNames.Contains(resourceName))
            {
                return Assembly.GetManifestResourceStream(resourceName);
            }
            throw new InvalidDataException("Resource \"" + resourceName + "\" not found.");
        }
    }
}