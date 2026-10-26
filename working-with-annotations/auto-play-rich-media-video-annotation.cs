using System;
using System.IO;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // source PDF
        const string outputPdf = "output_richmedia.pdf"; // result PDF
        const string mediaFile = "sample.mp4";         // media to play

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(mediaFile))
        {
            Console.Error.WriteLine($"Media file not found: {mediaFile}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the annotation rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);

            // Create the RichMediaAnnotation
            RichMediaAnnotation richAnno = new RichMediaAnnotation(page, rect);

            // ---------------------------------------------------------------------
            // 1) Set activation mode (if the property exists in the current library)
            // ---------------------------------------------------------------------
            PropertyInfo activationProp = typeof(RichMediaAnnotation).GetProperty("Activation");
            if (activationProp != null && activationProp.CanWrite)
            {
                // The property type is an enum – try to set it to "OnPageVisible"
                Type enumType = activationProp.PropertyType;
                try
                {
                    object enumValue = Enum.Parse(enumType, "OnPageVisible", ignoreCase: true);
                    activationProp.SetValue(richAnno, enumValue);
                }
                catch
                {
                    // If the enum value does not exist, ignore – the annotation will use the default activation.
                }
            }

            // ---------------------------------------------------------------------
            // 2) Attach the media file (using reflection to stay compatible with older versions)
            // ---------------------------------------------------------------------
            // Try the newer RichMediaContent API first
            Type richMediaContentType = Type.GetType("Aspose.Pdf.Annotations.RichMediaContent, Aspose.Pdf");
            if (richMediaContentType != null)
            {
                // Create an instance of RichMediaContent
                object richMediaContent = Activator.CreateInstance(richMediaContentType);

                // Look for an AddFile method – there are a few overloads in different versions
                MethodInfo addFileMethod = richMediaContentType.GetMethod("AddFile", new[] { typeof(string) }) ??
                                            richMediaContentType.GetMethod("AddFile", new[] { typeof(string), typeof(string) });
                if (addFileMethod != null)
                {
                    ParameterInfo[] parameters = addFileMethod.GetParameters();
                    if (parameters.Length == 1)
                    {
                        addFileMethod.Invoke(richMediaContent, new object[] { mediaFile });
                    }
                    else if (parameters.Length == 2)
                    {
                        // second parameter is usually the MIME type
                        addFileMethod.Invoke(richMediaContent, new object[] { mediaFile, "video/mp4" });
                    }
                }

                // Assign the content to the annotation (property may be named RichMediaContent)
                PropertyInfo contentProp = typeof(RichMediaAnnotation).GetProperty("RichMediaContent");
                if (contentProp != null && contentProp.CanWrite)
                {
                    contentProp.SetValue(richAnno, richMediaContent);
                }
            }
            else
            {
                // Fallback for very old versions – some releases expose a direct FilePath property
                PropertyInfo filePathProp = typeof(RichMediaAnnotation).GetProperty("FilePath");
                if (filePathProp != null && filePathProp.CanWrite)
                {
                    filePathProp.SetValue(richAnno, mediaFile);
                }
            }

            // Add the annotation to the page
            page.Annotations.Add(richAnno);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"RichMedia PDF saved to '{outputPdf}'.");
    }
}
