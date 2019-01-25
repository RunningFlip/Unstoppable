using UnityEngine;


/// <summary>
/// Helper class for easy vector calculations.
/// </summary>
public static class VectorExtensions
{
    //---------------------------------------------------------------------


    /// <summary>
    /// Adds two vectors.
    /// </summary>
    /// <param name="a">Vector that will be increased.</param>
    /// <param name="b">Vector to add.</param>
    /// <returns></returns>
    public static void AddVector(this Vector3 a, ref Vector3 b)
    {
        a.x += b.x;
        a.y += b.y;
        a.z += b.z;
    }

    /// <summary>
    /// Adds two vectors.
    /// </summary>
    /// <param name="a">Vector that will be increased.</param>
    /// <param name="b">Vector to add.</param>
    /// <returns></returns>
    public static Vector3 AddVectorOut(this Vector3 a, ref Vector3 b)
    {
        a.x += b.x;
        a.y += b.y;
        a.z += b.z;
        return a;
    }

    /// <summary>
    /// Increases the vector by a specific value.
    /// </summary>
    /// <param name="a">Vector that will be increased.</param>
    /// <param name="_add">Number to add.</param>
    /// <returns></returns>
    public static void AddVector(this Vector3 a, float _add)
    {
        a.x += _add;
        a.y += _add;
        a.z += _add;
    }

    /// <summary>
    /// Increases the vector by a specific value.
    /// </summary>
    /// <param name="a">Vector that will be increased.</param>
    /// <param name="_add">Number to add.</param>
    /// <returns></returns>
    public static Vector3 AddVectorOut(this Vector3 a, float _add)
    {
        a.x += _add;
        a.y += _add;
        a.z += _add;
        return a;
    }



    //---------------------------------------------------------------------



    /// <summary>
    /// Subtract two vectors.
    /// </summary>
    /// <param name="a">Vector that will be subtraced.</param>
    /// <param name="b">Vector to subtract.</param>
    /// <returns></returns>
    public static void SubVector(this Vector3 a, ref Vector3 b)
    {
        a.x -= b.x;
        a.y -= b.y;
        a.z -= b.z;
    }

    /// <summary>
    /// Subtract two vectors.
    /// </summary>
    /// <param name="a">Vector that will be subtraced.</param>
    /// <param name="b">Vector to subtract.</param>
    /// <returns></returns>
    public static Vector3 SubVectorOut(this Vector3 a, ref Vector3 b)
    {
        a.x -= b.x;
        a.y -= b.y;
        a.z -= b.z;
        return a;
    }

    /// <summary>
    /// Decreases the vector by a specific value.
    /// </summary>
    /// <param name="a">Vector that will be subtraced.</param>
    /// <param name="_sub">Number to subtract.</param>
    /// <returns></returns>
    public static void SubVector(this Vector3 a, float _sub)
    {
        a.x -= _sub;
        a.y -= _sub;
        a.z -= _sub;
    }

    /// <summary>
    /// Decreases the vector by a specific value.
    /// </summary>
    /// <param name="a">Vector that will be subtraced.</param>
    /// <param name="_sub">Number to subtract.</param>
    /// <returns></returns>
    public static Vector3 SubVectorOut(this Vector3 a, float _sub)
    {
        a.x -= _sub;
        a.y -= _sub;
        a.z -= _sub;
        return a;
    }



    //---------------------------------------------------------------------



    /// <summary>
    /// Multiplies two vectors.
    /// </summary>
    /// <param name="a">Vector that will be increased.</param>
    /// <param name="b">Vector to multiply with.</param>
    /// <returns></returns>
    public static void MultVector(this Vector3 a, ref Vector3 b)
    {
        a.x *= b.x;
        a.y *= b.y;
        a.z *= b.z;
    }

    /// <summary>
    /// Multiplies two vectors.
    /// </summary>
    /// <param name="a">Vector that will be increased.</param>
    /// <param name="b">Vector to multiply with.</param>
    /// <returns></returns>
    public static Vector3 MultVectorOut(this Vector3 a, ref Vector3 b)
    {
        a.x *= b.x;
        a.y *= b.y;
        a.z *= b.z;
        return a;
    }

    /// <summary>
    /// Multiplies the vector by a multiplier.
    /// </summary>
    /// <param name="a">Vector that will be increased.</param>
    /// <param name="_multiplier">Multiplier for the vector.</param>
    /// <returns></returns>
    public static void MultVector(this Vector3 a, float _multiplier)
    {
        a.x *= _multiplier;
        a.y *= _multiplier;
        a.z *= _multiplier;
    }

    /// <summary>
    /// Multiplies the vector by a multiplier.
    /// </summary>
    /// <param name="a">Vector that will be increased.</param>
    /// <param name="_multiplier">Multiplier for the vector.</param>
    /// <returns></returns>
    public static Vector3 MultVectorOut(this Vector3 a, float _multiplier)
    {
        a.x *= _multiplier;
        a.y *= _multiplier;
        a.z *= _multiplier;
        return a;
    }



    //---------------------------------------------------------------------



    /// <summary>
    /// Divides two vectors.
    /// </summary>
    /// <param name="a">Vector that will be divided.</param>
    /// <param name="b">Vector to divided.</param>
    /// <returns></returns>
    public static void DivVector(this Vector3 a, ref Vector3 b)
    {
        a.x /= b.x;
        a.y /= b.y;
        a.z /= b.z;
    }

    /// <summary>
    /// Divides two vectors.
    /// </summary>
    /// <param name="a">Vector that will be divided.</param>
    /// <param name="b">Vector to divided.</param>
    /// <returns></returns>
    public static Vector3 DivVectorOut(this Vector3 a, ref Vector3 b)
    {
        a.x /= b.x;
        a.y /= b.y;
        a.z /= b.z;
        return a;
    }

    /// <summary>
    /// Divides the vector by a divisior.
    /// </summary>
    /// <param name="a">Vector that will be divided.</param>
    /// <param name="_divisor">Divisor for the vector.</param>
    /// <returns></returns>
    public static void DivVectors(this Vector3 a, float _divisor)
    {
        a.x /= _divisor;
        a.y /= _divisor;
        a.z /= _divisor;
    }

    /// <summary>
    /// Divides the vector by a divisior.
    /// </summary>
    /// <param name="a">Vector that will be divided.</param>
    /// <param name="_divisor">Divisor for the vector.</param>
    /// <returns></returns>
    public static Vector3 DivVectorsOut(this Vector3 a, float _divisor)
    {
        a.x /= _divisor;
        a.y /= _divisor;
        a.z /= _divisor;
        return a;
    }



    //---------------------------------------------------------------------



    /// <summary>
    /// Conversts the Vector to the cross product of the vector itself and a second one.
    /// </summary>
    /// <param name="a">Vector that will be converted to the cross product.</param>
    /// <param name="b">Vector that will be used for the crossproduct.</param>
    public static void CrossProduct(this Vector3 a, ref Vector3 b)
    {
        Vector3 i = a; //Used as backup
        a.x = (i.y * b.z) - (i.z * b.y);
        a.y = (i.z * b.x) - (i.x * b.z);
        a.z = (i.x * b.y) - (i.y * b.x);
    }

    /// <summary>
    /// Conversts the Vector to the cross product of the vector itself and a second one.
    /// </summary>
    /// <param name="a">Vector that will be converted to the cross product.</param>
    /// <param name="b">Vector that will be used for the crossproduct.</param>
    public static Vector3 CrossProductOut(this Vector3 a, ref Vector3 b)
    {
        Vector3 i = a; //Used as backup
        a.x = (i.y * b.z) - (i.z * b.y);
        a.y = (i.z * b.x) - (i.x * b.z);
        a.z = (i.x * b.y) - (i.y * b.x);
        return i;
    }
}
