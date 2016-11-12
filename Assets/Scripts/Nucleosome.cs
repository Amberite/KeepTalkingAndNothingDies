using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Nucleosome
{
    string geneName;
    List<Histone> histones;

    public Nucleosome(string name, List<Histone> list)
    {
        geneName = name;
        histones = list;
    }
}
