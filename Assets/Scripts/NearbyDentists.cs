using System.Collections.Generic;
using System;

[Serializable]
public class NearbyDentists
{
    public List<NearbyDentist> results;
}

[Serializable]
public class NearbyDentist
{
    public string name;
    public char mapID;
    public string vicinity;
    public Geometry geometry;
}


[Serializable]
public struct Geometry
{
    public Location location;
}

[Serializable]
public struct Location
{
    public string lat;
    public string lng;
}