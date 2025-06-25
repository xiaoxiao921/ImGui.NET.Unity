using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace ImGuiNET
{
    public struct Vector2 : IEquatable<Vector2>, IFormattable
    {
        public float this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                float result;
                if (index != 0)
                {
                    if (index != 1)
                    {
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                    }
                    result = this.y;
                }
                else
                {
                    result = this.x;
                }
                return result;
            }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set
            {
                if (index != 0)
                {
                    if (index != 1)
                    {
                        throw new IndexOutOfRangeException("Invalid Vector2 index!");
                    }
                    this.y = value;
                }
                else
                {
                    this.x = value;
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Set(float newX, float newY)
        {
            this.x = newX;
            this.y = newY;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
        {
            return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
        {
            float num = target.x - current.x;
            float num2 = target.y - current.y;
            float num3 = num * num + num2 * num2;
            bool flag = num3 == 0f || (maxDistanceDelta >= 0f && num3 <= maxDistanceDelta * maxDistanceDelta);
            Vector2 result;
            if (flag)
            {
                result = target;
            }
            else
            {
                float num4 = (float)Math.Sqrt((double)num3);
                result = new Vector2(current.x + num / num4 * maxDistanceDelta, current.y + num2 / num4 * maxDistanceDelta);
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Scale(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Scale(Vector2 scale)
        {
            this.x *= scale.x;
            this.y *= scale.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Normalize()
        {
            float magnitude = this.magnitude;
            bool flag = magnitude > 1E-05f;
            if (flag)
            {
                this /= magnitude;
            }
            else
            {
                this = Vector2.zero;
            }
        }

        public Vector2 normalized
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                Vector2 result = new Vector2(this.x, this.y);
                result.Normalize();
                return result;
            }
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
            return string.Format("({0}, {1})", new object[]
            {
                this.x.ToString(format, formatProvider),
                this.y.ToString(format, formatProvider)
            });
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
        {
            return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override bool Equals(object other)
        {
            bool flag = !(other is Vector2);
            return !flag && this.Equals((Vector2)other);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Equals(Vector2 other)
        {
            return this.x == other.x && this.y == other.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
        {
            float num = -2f * Vector2.Dot(inNormal, inDirection);
            return new Vector2(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 Perpendicular(Vector2 inDirection)
        {
            return new Vector2(-inDirection.y, inDirection.x);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Dot(Vector2 lhs, Vector2 rhs)
        {
            return lhs.x * rhs.x + lhs.y * rhs.y;
        }

        public float magnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return (float)Math.Sqrt((double)(this.x * this.x + this.y * this.y));
            }
        }

        public float sqrMagnitude
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return this.x * this.x + this.y * this.y;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Distance(Vector2 a, Vector2 b)
        {
            float num = a.x - b.x;
            float num2 = a.y - b.y;
            return (float)Math.Sqrt((double)(num * num + num2 * num2));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
        {
            float sqrMagnitude = vector.sqrMagnitude;
            bool flag = sqrMagnitude > maxLength * maxLength;
            Vector2 result;
            if (flag)
            {
                float num = (float)Math.Sqrt((double)sqrMagnitude);
                float num2 = vector.x / num;
                float num3 = vector.y / num;
                result = new Vector2(num2 * maxLength, num3 * maxLength);
            }
            else
            {
                result = vector;
            }
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float SqrMagnitude(Vector2 a)
        {
            return a.x * a.x + a.y * a.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float SqrMagnitude()
        {
            return this.x * this.x + this.y * this.y;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x * b.x, a.y * b.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x / b.x, a.y / b.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator -(Vector2 a)
        {
            return new Vector2(-a.x, -a.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(Vector2 a, float d)
        {
            return new Vector2(a.x * d, a.y * d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(float d, Vector2 a)
        {
            return new Vector2(a.x * d, a.y * d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(Vector2 a, float d)
        {
            return new Vector2(a.x / d, a.y / d);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            float num = lhs.x - rhs.x;
            float num2 = lhs.y - rhs.y;
            return num * num + num2 * num2 < 9.9999994E-11f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return !(lhs == rhs);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector2(Vector3 v)
        {
            return new Vector2(v.x, v.y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator Vector3(Vector2 v)
        {
            return new Vector3(v.x, v.y, 0f);
        }

        public static Vector2 zero
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector2.zeroVector;
            }
        }

        public static Vector2 one
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector2.oneVector;
            }
        }

        public static Vector2 up
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector2.upVector;
            }
        }

        public static Vector2 down
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector2.downVector;
            }
        }

        public static Vector2 left
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector2.leftVector;
            }
        }

        public static Vector2 right
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector2.rightVector;
            }
        }

        public static Vector2 positiveInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector2.positiveInfinityVector;
            }
        }

        public static Vector2 negativeInfinity
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Vector2.negativeInfinityVector;
            }
        }

        // Note: this type is marked as 'beforefieldinit'.
        static Vector2()
        {
        }

        public float x;

        public float y;

        private static readonly Vector2 zeroVector = new Vector2(0f, 0f);

        private static readonly Vector2 oneVector = new Vector2(1f, 1f);

        private static readonly Vector2 upVector = new Vector2(0f, 1f);

        private static readonly Vector2 downVector = new Vector2(0f, -1f);

        private static readonly Vector2 leftVector = new Vector2(-1f, 0f);

        private static readonly Vector2 rightVector = new Vector2(1f, 0f);

        private static readonly Vector2 positiveInfinityVector = new Vector2(float.PositiveInfinity, float.PositiveInfinity);

        private static readonly Vector2 negativeInfinityVector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);

        public const float kEpsilon = 1E-05f;

        public const float kEpsilonNormalSqrt = 1E-15f;
    }
}
