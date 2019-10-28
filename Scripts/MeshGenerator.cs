using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
public class MeshGenerator_ : MonoBehaviour
{
    [SerializeField] float perlinX = 0.3f;
    [SerializeField] GameObject[] reCenterObjects;
 


    // G E N E R A T I V E 
    Mesh mesh;
    Vector3[] vertecies;
    int[] triangles;
    [SerializeField] float meshGriestiY  = 7.0f;

    public const int xPoints = 144;  // 1.44
    public const int zPoints = 120;  // 1.20
    [SerializeField] float riseForce = 0.05f;   
    [SerializeField] float sinkForce = -0.1f;
    [SerializeField] float maximumRise = 1;


    [SerializeField] public const int dataScale = 6;
    public const float visualScale = 5.92f;

    int xSize, zSize;
    int dataArraySize;

    public static float[] reliefValues;     // global data array

    [SerializeField] bool addPerlinNoise = true;
    Vector2[] UVs;  // texturai


    private static LineRenderer audioLine;

    MeshCollider meshCollider;
    // converts world coordinates to data array size
    public static Vector2 getArrayPointFromMeshSpace(float x, float z)
    {
        Vector2 v = new Vector2((x * dataScale) / visualScale, (z * dataScale) / visualScale);
        return v;
    }


    public static void updateAudioLine(Vector3[] array, int positions)
    {
         audioLine.positionCount = positions;
        audioLine.SetPositions(array);
        audioLine.enabled = true;
    }


    void Start()
    {
        //meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider = gameObject.GetComponent<MeshCollider>();
        xSize = (xPoints / dataScale) - 1;
        zSize = (zPoints / dataScale) - 1;
        dataArraySize = xPoints * zPoints;

        reliefValues = new float[dataArraySize];

        for (int i = 0; i < dataArraySize; i++)
        {
            reliefValues[i] = 0.0f;
        }

        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        CreateShape();
        UpdateMesh();

        audioLine = GetComponent<LineRenderer>();
        audioLine.enabled = false;

        // recenter objects relevant to generatedMesh
        foreach(GameObject go  in reCenterObjects)
        {
         
            go.transform.position += new Vector3((xPoints / dataScale * visualScale) / 2, 0, 0);
            

        }    
    }

    private void Update()
    {
        CreateShape();
        UpdateMesh();
    }


    void CreateShape()
    {

        Vector3 robotPosition = GameObject.FindWithTag("robot").transform.position;
        //Vector2 robotDataPoint = new Vector2((robotPosition.x * dataScale) / visualScale, (robotPosition.z * dataScale) / visualScale);
        Vector2 robotDataPoint = getArrayPointFromMeshSpace(robotPosition.x, robotPosition.z);

        //Debug.Log(string.Format("x:{0} y:{1}", robotDataPoint.x, robotDataPoint.y));

        // check if robot is in correct range, mapping or scaling may be needed for final version
        if ((robotDataPoint.x >= 0) && (robotDataPoint.x < xPoints) && (robotDataPoint.y >= 0) && (robotDataPoint.y < zPoints))
        {
            // update data array
            for (int xd = 0; xd < xPoints; xd++)
            {
                for (int zd = 0; zd < zPoints; zd++)
                {
                    int i = zd * xPoints + xd;
                    Vector2 dataPoint = new Vector2(xd, zd);
                    float distance = Vector2.Distance(robotDataPoint, dataPoint);

                    // read height value from array
                    float p = reliefValues[i];

                    // rise only closest points
                    if (distance < 10)
                    {
                        //float d = (10.0f / distance);
                        //p = p + (1 - p) * d * riseForce * Time.deltaTime;
                        p = p + riseForce * Time.deltaTime;
                    }

                    // sink all points
                    p = p + sinkForce * Time.deltaTime;

                    // add limits
                    if (p > meshGriestiY) p = meshGriestiY;
                    if (p < 0) p = 0;

                    // save new height value into array
                    reliefValues[i] = p;

                }
            }
        }


        // creates vertecies
        vertecies = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                    // take data from reliefValues array and modify mesh
                    int ii = (z * dataScale) * xPoints + (x * dataScale);
                    float y = reliefValues[ii];
                    vertecies[i] = new Vector3(x * visualScale, y, z * visualScale);

                if (addPerlinNoise)
                {
                    //float y = Mathf.PerlinNoise(x * perlinX, z * .3f) * 6f;
                    float yy = Mathf.PerlinNoise(x * perlinX, z ) * 8 ;
                    vertecies[i] = new Vector3(x * visualScale, yy, z * visualScale);

                }
                i++;
            }
            
        }

        // creates trienagles
        int vert = 0;
        int tris = 0;
        triangles = new int[xSize * zSize * 6];
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }

        // creates UV's
        UVs = new Vector2[vertecies.Length];

        for (int i = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                UVs[i] = new Vector2((float)x / xSize, (float)z / zSize);
                i++;
            }
        }
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertecies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.uv = UVs;
        meshCollider.sharedMesh = mesh;
    }

    private void OnDrawGizmos()
    {
        if (vertecies == null)
            return;

        for (int i = 0; i < vertecies.Length; i++)
        {
            Gizmos.DrawSphere(vertecies[i], 0.1f);
        }
    }
}
