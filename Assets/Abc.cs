using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abc : MonoBehaviour
{
    public GameObject go;
    public GameObject player;
    float ball_length = 0.7f;
    float extra_distance;
    int array_size = 50;
    Vector3[] arr;
    Vector2[] pol = new Vector2[51];
    Vector2 next;
    EdgeCollider2D poly_collide;
    float top, bottom;

    void Start()
    {
        arr = new Vector3[array_size];
        poly_collide = go.GetComponent<EdgeCollider2D>();
        
        arr[0].x = 0.9f;
        arr[0].y = 0;
        arr[array_size / 2].x = 3.1f;
        arr[array_size / 2].y = 0;
        pol[0].x = arr[0].x;
        pol[0].y = arr[0].y;
        pol[array_size].x = arr[0].x;
        pol[array_size].y = arr[0].y;
        for (int i = 1; i < array_size / 2; i++)
        {
            Vector2 two_points = Two_points(arr[i - 1].x, arr[i + array_size / 2 - 1].x);
            arr[i].x = two_points.x;
            arr[i + array_size / 2].x = two_points.y;
            arr[i].y = arr[i - 1].y - 1.5f;
            arr[i + array_size / 2].y = arr[i + array_size / 2 - 1].y - 1.5f;
            pol[i].x = arr[i].x;
            pol[i].y = arr[i].y;
            
        }

        for (int i = 1; i < 4; i++)
        {
            arr[i].x = 0.9f;
            arr[array_size / 2 + i].x = 3.1f;
            pol[i].x = 0.9f;
        }
        player.GetComponent<Transform>().position = new Vector3((arr[1].x + arr[array_size / 2].x) / 2, (arr[1].y + arr[array_size / 2].y) / 2, -5);
        for (int i = array_size - 1, j = array_size / 2; i >= array_size / 2; i--, j++)
        {
            pol[j].x = arr[i].x;
            pol[j].y = arr[i].y;
        }
        poly_collide.points = pol;
        go.GetComponent<MeshFilter>().mesh.vertices = arr;
        go.GetComponent<MeshFilter>().mesh.triangles = new int[] { 0, 1, 25, 25, 26, 1, 1, 2, 26, 26, 27, 2, 2, 3, 27, 27, 28, 3, 3, 4, 28, 28, 29, 4, 4, 5, 29, 29, 30, 5, 5, 6, 30, 30, 31, 6, 6, 7, 31, 31, 32, 7, 7, 8, 32, 32, 33, 8, 8, 9, 33, 33, 34, 9, 9, 10, 34, 34, 35, 10, 10, 11, 35, 35, 36, 11, 11, 12, 36, 36, 37, 12, 12, 13, 37, 37, 38, 13, 13, 14, 38, 38, 39, 14, 14, 15, 39, 39, 40, 15, 15, 16, 40, 40, 41, 16, 16, 17, 41, 41, 42, 17, 17, 18, 42, 42, 43, 18, 18, 19, 43, 43, 44, 19, 19, 20, 44, 44, 45, 20, 20, 21, 45, 45, 46, 21, 21, 22, 46, 46, 47, 22, 22, 23, 47, 47, 48, 23, 23, 24, 48, 48, 49, 24 };
        
        top = arr[0].y;
        bottom = arr[array_size - 1].y;
        for (int i = 0; i < array_size/2; i++)
        {
            Debug.Log(arr[i] + " " + arr[i + 25]);
        }
    }

    void Update()
    {

        top = arr[0].y;
        bottom = arr[array_size - 1].y;
        if (player.transform.position.y<(top+bottom)/2)
        {
            for (int i = 0; i < (array_size / 2) - 1; i++) 
            {
                arr[i] = arr[i + 1];
                pol[i].x = arr[i].x;
                pol[i].y = arr[i].y;
            }

            for (int i = array_size / 2; i < array_size - 1; i++) 
            {
                arr[i] = arr[i + 1];
            }
            Vector2 two_points = Two_points(arr[(array_size / 2) - 2].x, arr[(array_size / 2) - 2].x);
            arr[(array_size / 2) - 1].x = two_points.x;
            arr[array_size - 1].x = two_points.y;
            arr[(array_size / 2) - 1].y = arr[(array_size / 2) - 2].y - 1.5f;
            arr[array_size - 1].y = arr[array_size - 2].y - 1.5f;
            pol[(array_size / 2) - 1].x = arr[(array_size / 2) - 1].x;
            pol[(array_size / 2) - 1].y = arr[(array_size / 2) - 1].y;
            for (int i = array_size - 1, j = array_size / 2; i >= array_size / 2; i--, j++)
            {
                pol[j].x = arr[i].x;
                pol[j].y = arr[i].y;
            }
            pol[array_size].x = arr[0].x;
            pol[array_size].y = arr[0].y;

            poly_collide.points = pol;
            go.GetComponent<MeshFilter>().mesh.Clear();
            go.GetComponent<MeshFilter>().mesh.vertices = arr;
            go.GetComponent<MeshFilter>().mesh.triangles = new int[] { 0, 1, 25, 25, 26, 1, 1, 2, 26, 26, 27, 2, 2, 3, 27, 27, 28, 3, 3, 4, 28, 28, 29, 4, 4, 5, 29, 29, 30, 5, 5, 6, 30, 30, 31, 6, 6, 7, 31, 31, 32, 7, 7, 8, 32, 32, 33, 8, 8, 9, 33, 33, 34, 9, 9, 10, 34, 34, 35, 10, 10, 11, 35, 35, 36, 11, 11, 12, 36, 36, 37, 12, 12, 13, 37, 37, 38, 13, 13, 14, 38, 38, 39, 14, 14, 15, 39, 39, 40, 15, 15, 16, 40, 40, 41, 16, 16, 17, 41, 41, 42, 17, 17, 18, 42, 42, 43, 18, 18, 19, 43, 43, 44, 19, 19, 20, 44, 44, 45, 20, 20, 21, 45, 45, 46, 21, 21, 22, 46, 46, 47, 22, 22, 23, 47, 47, 48, 23, 23, 24, 48, 48, 49, 24 };
            
        }
    }

    Vector2 Two_points(float x1, float x2)
    {
        next.x = Random.Range(x1 - 2.2f, x1 + 2.2f);
        next.y = next.x + 2 * Random.Range(ball_length + extra_distance, 2.5f * ball_length);
        return next;
    }
}
