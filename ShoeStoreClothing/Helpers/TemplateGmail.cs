﻿namespace ShoeStoreClothing.Helpers
{
    public class TemplateGmail
    {
        public static string template = @"<body>
    <div class='card mt-50 mb-50'>
        <div class='col d-flex'><span class='text-muted' id='orderno'>order #546924</span></div>
        <div class='gap'>
            <div class='col-2 d-flex mx-auto'> </div>
        </div>
        <div class='title mx-auto'> Thank you for your order! </div>
        <div class='main'> <span id='sub-title'>
                <p><b>Payment Summary</b></p>
            </span>
            <div class='row row-main'>
                <div class='col-3'> <img class='img-fluid' src='https://i.imgur.com/qSnCFIS.png'> </div>
                <div class='col-6'>
                    <div class='row d-flex'>
                        <p><b>iPhone XR</b></p>
                    </div>
                    <div class='row d-flex'>
                        <p class='text-muted'>128GB White</p>
                    </div>
                </div>
                <div class='col-3 d-flex justify-content-end'>
                    <p><b>$599</b></p>
                </div>
            </div>
            <div class='row row-main'>
                <div class='col-3'> <img class='img-fluid' src='https://i.imgur.com/WuJwAJD.jpg'> </div>
                <div class='col-6'>
                    <div class='row d-flex'>
                        <p><b>Airpods</b></p>
                    </div>
                    <div class='row d-flex'>
                        <p class='text-muted'>With charging case</p>
                    </div>
                </div>
                <div class='col-3 d-flex justify-content-end'>
                    <p><b>$199</b></p>
                </div>
            </div>
            <div class='row row-main'>
                <div class='col-3 '> <img class='img-fluid' src='https://i.imgur.com/hOsIes2.png'> </div>
                <div class='col-6'>
                    <div class='row d-flex'>
                        <p><b>Belkin Boost Up</b></p>
                    </div>
                    <div class='row d-flex'>
                        <p class='text-muted'>Wireless charging pad</p>
                    </div>
                </div>
                <div class='col-3 d-flex justify-content-end'>
                    <p><b>$49.95</b></p>
                </div>
            </div>
            <hr>
            <div class='total'>
                <div class='row'>
                    <div class='col'> <b> Total:</b> </div>
                    <div class='col d-flex justify-content-end'> <b>$847.95</b> </div>
                </div> <button class='btn d-flex mx-auto'> Track your order </button>
            </div>
        </div>
    </div>
    
</body>";
    }
}
