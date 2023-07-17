# Programmer tool for Lattice iCE5 (iCE40 Ultra) FPGA

USB Custom HID interface.
FPGA bitstream is uploaded to FPGA SRAM!

![GUI screenshot](assets/gui.png)

## Supported hardware
1. STM32F3Discovery
![STM32F3Discovery photo](assets/stm32f3discovery.png)

[Firmware](firmware/stm32f303/stm32f3lattice_disc.hex)

Pinout:
| Pin | Func  | 
|-----|-------|
| PA5 | SCK   | 
| PA7 | MOSI  |
| PB0 | CS    |
| PB1 | CRST  |
| PB2 | CDONE |
| PE8 | LED   |

2. [STM32F303 + ice5 Development Board](http://ebrombaugh.studionebula.com/embedded/f303_ice5/)
![STM32F303 + ice5 Development Board photo](assets/ice5devboard.png)

[Firmware](firmware/stm32f303/stm32f3lattice_ice5.hex)

Pinout:
| Pin | Func  | 
|-----|-------|
| PA5 | SCK   | 
| PA7 | MOSI  |
| PB0 | CS    |
| PB1 | CRST  |
| PB2 | CDONE |
| PA9 | LED   |
