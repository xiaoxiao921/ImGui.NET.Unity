using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ImGuiNET
{
    public struct Vector3 : IEquatable<Vector3>, IFormattable
    {
        public float this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                float result;
                switch (index)
                {
                    case 0:
                        result = this.x;
                        break;
                    case 1:
                        result = this.y;
                        break;
                    case 2:
                        result = this.z;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
                return result;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                switch (index)
                {
                    case 0:
                        this.x = value;
                        break;
                    case 1:
                        this.y = value;
                        break;
                    case 2:
                        this.z = value;
                        break;
                    default:
                        throw new IndexOutOfRangeException("Invalid Vector3 index!");
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector3(float x, float y)
        {
            this.x = x;
            this.y = y;
            this.z = 0f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(float newX, float newY, float newZ)
        {
            this.x = newX;
            this.y = newY;
            this.z = newZ;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Scale(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Scale(Vector3 scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
            this.z *= scale.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2 ^ this.z.GetHashCode() >> 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other)
        {
            bool flag = !(other is Vector3);
            return !flag && this.Equals((Vector3)other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector3 other)
        {
            return this.x == other.x && this.y == other.y && this.z == other.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
        {
            float num = -2f * Vector3.Dot(inNormal, inDirection);
            return new Vector3(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y, num * inNormal.z + inDirection.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 Normalize(Vector3 value)
        {
            float num = Vector3.Magnitude(value);
            bool flag = num > 1E-05f;
            Vector3 result;
            if (flag)
            {
                result = value / num;
            }
            else
            {
                result = Vector3.zero;
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
        {
            float num = Vector3.Magnitude(this);
            bool flag = num > 1E-05f;
            if (flag)
            {
                this /= num;
            }
            else
            {
                this = Vector3.zero;
            }
        }

        public Vector3 normalized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector3.Normalize(this);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vector3 lhs, Vector3 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector3 a, Vector3 b)
        {
            float num = a.x - b.x;
            float num2 = a.y - b.y;
            float num3 = a.z - b.z;
            return (float)Math.Sqrt((double)(num * num + num2 * num2 + num3 * num3));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
        {
            float sqrMagnitude = vector.sqrMagnitude;
            bool flag = sqrMagnitude > maxLength * maxLength;
            Vector3 result;
            if (flag)
            {
                float num = (float)Math.Sqrt((double)sqrMagnitude);
                float num2 = vector.x / num;
                float num3 = vector.y / num;
                float num4 = vector.z / num;
                result = new Vector3(num2 * maxLength, num3 * maxLength, num4 * maxLength);
            }
            else
            {
                result = vector;
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Magnitude(Vector3 vector)
        {
            return (float)Math.Sqrt((double)(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z));
        }

        public float magnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (float)Math.Sqrt((double)(this.x * this.x + this.y * this.y + this.z * this.z));
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SqrMagnitude(Vector3 vector)
        {
            return vector.x * vector.x + vector.y * vector.y + vector.z * vector.z;
        }

        public float sqrMagnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return this.x * this.x + this.y * this.y + this.z * this.z;
            }
        }

        public static Vector3 zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector3.zeroVector;
            }
        }

        public static Vector3 one
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector3.oneVector;
            }
        }

        public static Vector3 forward
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector3.forwardVector;
            }
        }

        public static Vector3 back
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector3.backVector;
            }
        }

        public static Vector3 up
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector3.upVector;
            }
        }

        public static Vector3 down
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector3.downVector;
            }
        }

        public static Vector3 left
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector3.leftVector;
            }
        }

        public static Vector3 right
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector3.rightVector;
            }
        }

        public static Vector3 positiveInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector3.positiveInfinityVector;
            }
        }

        public static Vector3 negativeInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector3.negativeInfinityVector;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator -(Vector3 a)
        {
            return new Vector3(-a.x, -a.y, -a.z);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(Vector3 a, float d)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator *(float d, Vector3 a)
        {
            return new Vector3(a.x * d, a.y * d, a.z * d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector3 operator /(Vector3 a, float d)
        {
            return new Vector3(a.x / d, a.y / d, a.z / d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            float num = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            float num3 = lhs.z - rhs.z;
            float num4 = num * num + num2 * num2 + num3 * num3;
            return num4 < 9.9999994E-11f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return !(lhs == rhs);
        }

        public override string ToString()
        {
            return this.ToString(null, null);
        }

        public string ToString(string format)
        {
            return this.ToString(format, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            bool flag = string.IsNullOrEmpty(format);
            if (flag)
            {
                format = "F2";
            }
            bool flag2 = formatProvider == null;
            if (flag2)
            {
                formatProvider = CultureInfo.InvariantCulture.NumberFormat;
            }
            return string.Format("({0}, {1}, {2})", new object[]
            {
                this.x.ToString(format, formatProvider),
                this.y.ToString(format, formatProvider),
                this.z.ToString(format, formatProvider)
            });
        }

        public const float kEpsilon = 1E-05f;

        public const float kEpsilonNormalSqrt = 1E-15f;

        public float x;

        public float y;

        public float z;

        private static readonly Vector3 zeroVector = new Vector3(0f, 0f, 0f);

        private static readonly Vector3 oneVector = new Vector3(1f, 1f, 1f);

        private static readonly Vector3 upVector = new Vector3(0f, 1f, 0f);

        private static readonly Vector3 downVector = new Vector3(0f, -1f, 0f);

        private static readonly Vector3 leftVector = new Vector3(-1f, 0f, 0f);

        private static readonly Vector3 rightVector = new Vector3(1f, 0f, 0f);

        private static readonly Vector3 forwardVector = new Vector3(0f, 0f, 1f);

        private static readonly Vector3 backVector = new Vector3(0f, 0f, -1f);

        private static readonly Vector3 positiveInfinityVector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

        private static readonly Vector3 negativeInfinityVector = new Vector3(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
    }
}
