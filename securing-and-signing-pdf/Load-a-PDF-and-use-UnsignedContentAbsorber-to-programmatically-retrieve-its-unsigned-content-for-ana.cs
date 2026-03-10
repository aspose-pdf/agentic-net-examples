using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF file using the PdfFileSignature facade
        using (PdfFileSignature pdfSignature = new PdfFileSignature())
        {
            pdfSignature.BindPdf(inputPath);

            // Try to extract unsigned content using the API that is available in the
            // referenced Aspose.Pdf version.  The class UnsignedContentAbsorber and the
            // method ExtractUnsignedContent(UnsignedContentAbsorber) are present only in
            // newer releases.  To keep the code compilable with older releases we use
            // reflection – if the types/methods exist we use them, otherwise we fall back
            // to the parameter‑less ExtractUnsignedContent method (available in older
            // versions) or simply report that the feature is unavailable.
            string unsignedContent = ExtractUnsignedContentWithFallback(pdfSignature);

            Console.WriteLine("=== Unsigned Content ===");
            Console.WriteLine(unsignedContent);
        }
    }

    private static string ExtractUnsignedContentWithFallback(PdfFileSignature pdfSignature)
    {
        // 1️⃣ Try the modern API: UnsignedContentAbsorber + ExtractUnsignedContent(absorber)
        Type absorberType = Type.GetType("Aspose.Pdf.Facades.UnsignedContentAbsorber, Aspose.Pdf");
        if (absorberType != null)
        {
            // Create an instance of UnsignedContentAbsorber
            object absorber = Activator.CreateInstance(absorberType);
            // Locate the method that accepts the absorber
            MethodInfo extractMethod = typeof(PdfFileSignature).GetMethod(
                "ExtractUnsignedContent",
                new[] { absorberType }
            );
            if (extractMethod != null)
            {
                // Invoke the method – it populates the absorber instance
                extractMethod.Invoke(pdfSignature, new[] { absorber });
                // Retrieve the UnsignedContent property from the absorber
                PropertyInfo contentProp = absorberType.GetProperty("UnsignedContent");
                if (contentProp != null)
                {
                    return contentProp.GetValue(absorber) as string ?? string.Empty;
                }
            }
        }

        // 2️⃣ Fallback for older versions: a parameter‑less ExtractUnsignedContent method
        MethodInfo fallbackMethod = typeof(PdfFileSignature).GetMethod(
            "ExtractUnsignedContent",
            Type.EmptyTypes
        );
        if (fallbackMethod != null)
        {
            object result = fallbackMethod.Invoke(pdfSignature, null);
            return result as string ?? string.Empty;
        }

        // 3️⃣ If neither API is available, inform the caller.
        return "Unsigned content extraction is not supported by the referenced Aspose.Pdf version.";
    }
}
