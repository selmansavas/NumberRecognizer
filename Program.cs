using System;
using System.IO;
using System.Threading;


namespace ReadMNIST
{
  public class Constants
  {
    public const int InputSize = 764;
    public const int OutputSize = 10;
  }
  class Neuron
  {
    double [] input;
    double [] weight;
    double output;

    public Neuron()
    {
      input = new double [Constants.InputSize];
      
      

      for(int i = 0; i <input.Length;i++)
      {
        input[i] = 0;
      }

      weight = new double [Constants.InputSize];

      Random random = new Random();

      for(int i = 0; i <weight.Length;i++)
      {
        weight[i] = random.Next(0,1);
      }

      output = 0;
    }
  }

   class Layer
  {
    public Neuron [] layer;

    public Layer()
    {
      layer = new Neuron[Constants.OutputSize];

    }
  }



  class Output
  {
    public int [] outputArray;

    public Output()
    {
      outputArray = new int [Constants.OutputSize];
      for(int i = 0; i < outputArray.Length;i++)
      {
        outputArray[i] = 0;
      }
    }
  }


  class Program
  {
    static void Main(string[] args)
    {
     // Layer firstLayer = new Layer();
      MnistDecoder mnsitDecoder = new MnistDecoder();
    } 
  } 

  class MnistDecoder
  {
    public MnistDecoder()
    {
        try
      {
        Console.WriteLine("\nBegin\n");
        FileStream ifsLabels =
         new FileStream(@"C:\Users\Selman\Documents\GitHub\mnist-1lnn\data\t10k-labels-idx1-ubyte",
         FileMode.Open); // test labels
        FileStream ifsImages =
         new FileStream(@"C:\Users\Selman\Documents\GitHub\mnist-1lnn\data\t10k-images-idx3-ubyte",
         FileMode.Open); // test images

        BinaryReader brLabels =
         new BinaryReader(ifsLabels);
        BinaryReader brImages =
         new BinaryReader(ifsImages);
 
        int magic1 = brImages.ReadInt32(); // discard
        int numImages = brImages.ReadInt32(); 
        int numRows = brImages.ReadInt32(); 
        int numCols = brImages.ReadInt32(); 

        int magic2 = brLabels.ReadInt32(); 
        int numLabels = brLabels.ReadInt32(); 

        byte[][] pixels = new byte[28][];
        for (int i = 0; i < pixels.Length; ++i)
          pixels[i] = new byte[28];

        // each test image
        for (int di = 0; di < 10000; ++di) 
        {
          for (int i = 0; i < 28; i++)
          {
            for (int j = 0; j < 28; j++)
            {
              byte b = brImages.ReadByte();
              pixels[i][j] = b;
            }
          }

          byte lbl = brLabels.ReadByte();

          DigitImage dImage =
            new DigitImage(pixels, lbl);
          Console.WriteLine(dImage.ToString());
          //Console.ReadLine();
          Thread.Sleep(500);
        } // each image

        ifsImages.Close();
        brImages.Close();
        ifsLabels.Close();
        brLabels.Close();

        Console.WriteLine("\nEnd\n");
        Console.ReadLine();
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        Console.ReadLine();
      }
    }
     
  }


 
  public class DigitImage
  {
    public byte[][] pixels;
    public byte label;

    public DigitImage(byte[][] pixels,
      byte label)
    {
      this.pixels = new byte[28][];
      for (int i = 0; i < this.pixels.Length; ++i)
        this.pixels[i] = new byte[28];

      for (int i = 0; i < 28; ++i)
        for (int j = 0; j < 28; ++j)
          this.pixels[i][j] = pixels[i][j];

      this.label = label;
    }

    public override string ToString()
    {
      string s = "";
      for (int i = 0; i < 28; ++i)
      {
        for (int j = 0; j < 28; ++j)
        {
          if (this.pixels[i][j] == 0)
            s += "."; // white
          else if (this.pixels[i][j] == 255)
            s += "O"; // black
          else
            s += "X"; // gray
        }
        s += "\n";
      }
      s += this.label.ToString();
      return s;
    } // ToString

  }
} // ns

 