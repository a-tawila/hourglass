﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimerInput.cs" company="Chris Dziemborowicz">
//   Copyright (c) Chris Dziemborowicz. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Hourglass
{
    using System;

    /// <summary>
    /// A representation of an input for a <see cref="Timer"/>.
    /// </summary>
    public abstract class TimerInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimerInput"/> class.
        /// </summary>
        protected TimerInput()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerInput"/> class from a <see cref="TimerInputInfo"/>.
        /// </summary>
        /// <param name="inputInfo">A <see cref="TimerInputInfo"/>.</param>
        protected TimerInput(TimerInputInfo inputInfo)
        {
            if (inputInfo == null)
            {
                throw new ArgumentNullException("inputInfo");
            }
        }

        /// <summary>
        /// Returns a <see cref="TimerInput"/> for the specified <see cref="string"/>, or <c>null</c> if the <see
        /// cref="string"/> is not a valid input, favoring a <see cref="DateTimeTimerInput"/> in the case of ambiguity.
        /// </summary>
        /// <param name="str">An input <see cref="string"/>.</param>
        /// <returns>A <see cref="TimerInput"/> for the specified <see cref="string"/>, or <c>null</c> if the <see
        /// cref="string"/> is not a valid input.</returns>
        public static TimerInput FromDateTimeOrTimeSpan(string str)
        {
            DateTime dateTime;
            if (DateTimeUtility.TryParseNatural(str, out dateTime))
            {
                return new DateTimeTimerInput(dateTime);
            }

            TimeSpan timeSpan;
            if (TimeSpanUtility.TryParseNatural(str, out timeSpan))
            {
                return new TimeSpanTimerInput(timeSpan);
            }

            return null;
        }

        /// <summary>
        /// Returns a <see cref="TimerInput"/> for the specified <see cref="string"/>, or <c>null</c> if the <see
        /// cref="string"/> is not a valid input, favoring a <see cref="TimeSpanTimerInput"/> in the case of ambiguity.
        /// </summary>
        /// <param name="str">An input <see cref="string"/>.</param>
        /// <returns>A <see cref="TimerInput"/> for the specified <see cref="string"/>, or <c>null</c> if the <see
        /// cref="string"/> is not a valid input.</returns>
        public static TimerInput FromTimeSpanOrDateTime(string str)
        {
            TimeSpan timeSpan;
            if (TimeSpanUtility.TryParseNatural(str, out timeSpan))
            {
                return new TimeSpanTimerInput(timeSpan);
            }

            DateTime dateTime;
            if (DateTimeUtility.TryParseNatural(str, out dateTime))
            {
                return new DateTimeTimerInput(dateTime);
            }

            return null;
        }

        /// <summary>
        /// Returns a <see cref="TimerInput"/> for the specified <see cref="TimerInputInfo"/>, or <c>null</c> if the
        /// <see cref="TimerInputInfo"/> is not a supported type.
        /// </summary>
        /// <param name="inputInfo">A <see cref="TimerInputInfo"/>.</param>
        /// <returns>A <see cref="TimerInput"/> for the specified <see cref="TimerInputInfo"/>, or <c>null</c> if the
        /// <see cref="TimerInputInfo"/> is not a supported type.</returns>
        public static TimerInput FromTimerInputInfo(TimerInputInfo inputInfo)
        {
            TimeSpanTimerInputInfo timeSpanTimerInputInfo = inputInfo as TimeSpanTimerInputInfo;
            if (timeSpanTimerInputInfo != null)
            {
                return new TimeSpanTimerInput(timeSpanTimerInputInfo);
            }

            DateTimeTimerInputInfo dateTimeTimerInputInfo = inputInfo as DateTimeTimerInputInfo;
            if (dateTimeTimerInputInfo != null)
            {
                return new DateTimeTimerInput(dateTimeTimerInputInfo);
            }

            return null;
        }

        /// <summary>
        /// Returns the representation of the <see cref="TimerInput"/> used for XML serialization.
        /// </summary>
        /// <returns>The representation of the <see cref="TimerInput"/> used for XML serialization.</returns>
        public abstract TimerInputInfo ToTimerInputInfo();
    }
}