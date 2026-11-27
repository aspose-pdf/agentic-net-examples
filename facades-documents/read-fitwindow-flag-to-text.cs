using System;
using System.IO;
using Aspose.Pdf;
using Microsoft.CSharp.RuntimeBinder; // needed for RuntimeBinderException

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "fitwindow.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF directly with Aspose.Pdf.Document. PdfContentEditor is not required for reading ViewerPreferences.
        using (Document doc = new Document(inputPdf))
        {
            bool fitWindow = false;

            // ViewerPreferences was introduced in later versions of Aspose.Pdf.
            // To keep the code compatible with older versions we access it via dynamic and handle the possible RuntimeBinderException.
            try
            {
                dynamic dynDoc = doc;
                var prefs = dynDoc.ViewerPreferences; // may throw at runtime if the property does not exist
                if (prefs != null)
                {
                    fitWindow = prefs.FitWindow;
                }
            }
            catch (RuntimeBinderException)
            {
                // The current Aspose.Pdf version does not support ViewerPreferences.
                // Default to false and continue.
                fitWindow = false;
            }

            // Write the flag value to a plain‑text file.
            string result = $"FitWindow: {fitWindow}";
            File.WriteAllText(outputTxt, result);
        }

        Console.WriteLine($"FitWindow flag saved to '{outputTxt}'.");
    }
}
