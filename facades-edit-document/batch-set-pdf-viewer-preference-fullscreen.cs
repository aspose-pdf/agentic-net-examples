using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf.Facades; // PdfFileMend is in this namespace

class Program
{
    static void Main()
    {
        // Directory containing the PDF files to process
        const string inputDirectory = @"C:\PdfFolder";

        if (!Directory.Exists(inputDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {inputDirectory}");
            return;
        }

        // Process each PDF file in the directory
        foreach (string pdfPath in Directory.GetFiles(inputDirectory, "*.pdf"))
        {
            try
            {
                // Temporary file to hold the modified PDF
                string tempPath = Path.Combine(
                    Path.GetDirectoryName(pdfPath),
                    Path.GetFileNameWithoutExtension(pdfPath) + "_temp.pdf");

                // Try to obtain the ViewerPreferences type via reflection (it may not exist in older Aspose.Pdf versions)
                Type viewerPrefsType = Type.GetType("Aspose.Pdf.ViewerPreferences, Aspose.Pdf");
                if (viewerPrefsType == null)
                {
                    Console.Error.WriteLine($"ViewerPreferences type not found in Aspose.Pdf assembly. Skipping file: {pdfPath}");
                    continue;
                }

                // Create an instance of ViewerPreferences and set FullScreen = true using reflection
                object viewerPrefsInstance = Activator.CreateInstance(viewerPrefsType);
                PropertyInfo fullScreenProp = viewerPrefsType.GetProperty("FullScreen");
                if (fullScreenProp != null && fullScreenProp.CanWrite)
                {
                    fullScreenProp.SetValue(viewerPrefsInstance, true);
                }
                else
                {
                    Console.Error.WriteLine($"FullScreen property not found on ViewerPreferences. Skipping file: {pdfPath}");
                    continue;
                }

                // Locate the SetViewerPreferences method that matches the runtime ViewerPreferences type
                MethodInfo setViewerMethod = typeof(PdfFileMend).GetMethod(
                    "SetViewerPreferences",
                    new[] { typeof(string), typeof(string), viewerPrefsType });

                if (setViewerMethod == null)
                {
                    Console.Error.WriteLine($"SetViewerPreferences method overload not found. Skipping file: {pdfPath}");
                    continue;
                }

                // Invoke the method via reflection
                using (PdfFileMend mend = new PdfFileMend())
                {
                    setViewerMethod.Invoke(mend, new object[] { pdfPath, tempPath, viewerPrefsInstance });
                }

                // Replace the original file with the modified one
                File.Delete(pdfPath);
                File.Move(tempPath, pdfPath);

                Console.WriteLine($"Updated full‑screen preference: {pdfPath}");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing '{pdfPath}': {ex.Message}");
            }
        }
    }
}
