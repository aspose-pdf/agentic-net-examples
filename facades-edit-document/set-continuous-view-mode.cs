using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "continuous_view.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document pdfDocument = new Document(inputPath);

            // Attempt to set continuous view mode using ViewerPreferences.
            // The ViewerPreferences property exists only in newer Aspose.Pdf versions.
            // Access it via dynamic to keep the code compilable with older versions.
            try
            {
                dynamic docDyn = pdfDocument;

                // Ensure the ViewerPreferences object exists (newer versions create it lazily).
                if (docDyn.ViewerPreferences == null)
                {
                    // Use reflection to avoid a compile‑time dependency on the ViewerPreferences type.
                    // This works even if the type is absent in older library versions.
                    const string vpFullName = "Aspose.Pdf.ViewerPreferences, Aspose.Pdf";
                    Type vpType = Type.GetType(vpFullName);
                    if (vpType != null)
                    {
                        docDyn.ViewerPreferences = Activator.CreateInstance(vpType);
                    }
                    else
                    {
                        // If the type cannot be found, we simply skip setting preferences.
                        Console.WriteLine("ViewerPreferences type is not available in the current Aspose.Pdf library version. Continuous view mode will not be applied.");
                    }
                }

                // Set continuous scrolling (OneColumn layout) if the property is present.
                // This will be a runtime operation; if the property does not exist, a RuntimeBinderException is thrown.
                docDyn.ViewerPreferences.PageLayout = PageLayout.OneColumn;
            }
            catch (RuntimeBinderException)
            {
                Console.WriteLine("ViewerPreferences property is not available in the current Aspose.Pdf library version. Continuous view mode will not be applied.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error while configuring viewer preferences: {ex.Message}");
            }

            // Save the modified PDF
            pdfDocument.Save(outputPath);

            Console.WriteLine($"Continuous view mode applied (if supported). Output saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
