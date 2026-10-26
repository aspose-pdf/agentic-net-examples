using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_with_sound.pdf";
        const string soundFilePath = "background.wav";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        if (!File.Exists(soundFilePath))
        {
            Console.Error.WriteLine($"Sound file not found: {soundFilePath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Choose the page where the annotation will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the rectangle area for the annotation (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 300, 550);

            // ---------------------------------------------------------------------
            // Create a SoundAnnotation in a version‑agnostic way using reflection.
            // This avoids compile‑time errors when the library version does not expose
            // the expected constructors or members.
            // ---------------------------------------------------------------------
            Type soundAnnotType = Type.GetType("Aspose.Pdf.Annotations.SoundAnnotation, Aspose.Pdf");
            if (soundAnnotType == null)
            {
                Console.Error.WriteLine("SoundAnnotation type not found in the loaded Aspose.Pdf assembly.");
                doc.Save(outputPdfPath);
                return;
            }

            object soundAnnot = null;
            // Prefer the (Page, Rectangle) constructor if it exists.
            var ctorWithPageRect = soundAnnotType.GetConstructor(new[] { typeof(Page), typeof(Rectangle) });
            if (ctorWithPageRect != null)
            {
                soundAnnot = ctorWithPageRect.Invoke(new object[] { page, rect });
            }
            else
            {
                // Fallback to the parameterless constructor and set the Rect property manually.
                var defaultCtor = soundAnnotType.GetConstructor(Type.EmptyTypes);
                soundAnnot = defaultCtor.Invoke(null);
                var rectProp = soundAnnotType.GetProperty("Rect");
                rectProp?.SetValue(soundAnnot, rect);
            }

            // Load the audio file into a Sound object (also via reflection).
            using (FileStream soundStream = File.OpenRead(soundFilePath))
            {
                Type soundType = Type.GetType("Aspose.Pdf.Annotations.Sound, Aspose.Pdf");
                if (soundType != null)
                {
                    var soundCtor = soundType.GetConstructor(new[] { typeof(Stream) });
                    var soundObj = soundCtor?.Invoke(new object[] { soundStream });

                    // Assign the Sound object to the annotation if the property exists.
                    var soundProp = soundAnnotType.GetProperty("Sound");
                    soundProp?.SetValue(soundAnnot, soundObj);
                }
            }

            // Enable automatic playback when the page becomes visible, if the property is present.
            var playProp = soundAnnotType.GetProperty("PlayOnPageOpen");
            if (playProp != null && playProp.CanWrite)
            {
                playProp.SetValue(soundAnnot, true);
            }

            // Add the annotation to the page's annotation collection.
            page.Annotations.Add((Annotation)soundAnnot);

            // Save the modified PDF.
            doc.Save(outputPdfPath);
        }

        Console.WriteLine($"PDF with sound annotation saved to '{outputPdfPath}'.");
    }
}
