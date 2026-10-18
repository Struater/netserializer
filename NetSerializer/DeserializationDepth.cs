using System;
using System.IO;

namespace NetSerializer
{
	// Dumont changes start
	public sealed class DeserializationDepthException : InvalidDataException
	{
		public DeserializationDepthException(int max)
			: base($"Object graph is deeper than {max} levels")
		{
		}
	}

	public static class DeserializationDepth
	{
		public const int Max = 512;

		[ThreadStatic]
		static int t_depth;

		public static void Enter()
		{
			if (++t_depth > Max)
				throw new DeserializationDepthException(Max);
		}

		public static void Exit()
		{
			t_depth--;
		}

		internal static int Save()
		{
			var depth = t_depth;
			t_depth = 0;
			return depth;
		}

		internal static void Restore(int depth)
		{
			t_depth = depth;
		}
	}
	// Dumont end
}
