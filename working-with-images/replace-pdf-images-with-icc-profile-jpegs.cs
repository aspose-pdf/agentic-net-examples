using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output_icc.pdf";
        const string iccPath = "profile.icc";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(iccPath))
        {
            Console.Error.WriteLine($"ICC profile not found: {iccPath}");
            return;
        }

        // Load ICC profile bytes once
        byte[] iccData = File.ReadAllBytes(iccPath);

        // Open PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Iterate through all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Iterate over all images on the page
                foreach (XImage img in page.Resources.Images)
                {
                    // Use reflection to access ImageInfo and its IccProfile property.
                    // This works across Aspose.Pdf versions where ImageInfo may be missing.
                    Type imgType = img.GetType();
                    PropertyInfo imageInfoProp = imgType.GetProperty("ImageInfo", BindingFlags.Public | BindingFlags.Instance);
                    if (imageInfoProp != null)
                    {
                        // Get or create ImageInfo instance
                        object imageInfo = imageInfoProp.GetValue(img);
                        if (imageInfo == null)
                        {
                            // ImageInfo type is usually Aspose.Pdf.ImageInfo
                            Type imageInfoType = imgType.Assembly.GetType("Aspose.Pdf.ImageInfo");
                            if (imageInfoType != null)
                            {
                                imageInfo = Activator.CreateInstance(imageInfoType);
                                imageInfoProp.SetValue(img, imageInfo);
                            }
                        }

                        if (imageInfo != null)
                        {
                            // Set the ICC profile if the property exists
                            PropertyInfo iccProp = imageInfo.GetType().GetProperty("IccProfile", BindingFlags.Public | BindingFlags.Instance);
                            if (iccProp != null && iccProp.CanWrite)
                            {
                                iccProp.SetValue(imageInfo, iccData);
                            }
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with embedded ICC profiles: {outputPdf}");
    }
}
